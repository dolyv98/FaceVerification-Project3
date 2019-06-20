using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;


namespace CognitiveServices.FaceAPI.Verification
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string folderPath;
       

        public string testlink = "" ;
        String[] lines = File.ReadAllLines(@"E:\20182\CognitiveServices-Samples-master\assets\list.txt");


        public Window1()
        {
            InitializeComponent();
            Lock.IsEnabled = false;
            Unlock.IsEnabled = false;
            Open.IsEnabled = false;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow m1 = new MainWindow(testlink, 0, lines1);
            //m1.Show();
            Capture c1 = new Capture(1, folderPath);
            c1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MainWindow m1 = new MainWindow(testlink, 2, lines1);
            //m1.Show();
            Capture c2 = new Capture(2, folderPath);
            c2.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog cfd = new CommonOpenFileDialog();
            cfd.InitialDirectory = "C:\\Users";
            cfd.IsFolderPicker = true;
            if (cfd.ShowDialog() == CommonFileDialogResult.Ok)

            {
                int i = 0;
                folderPath = cfd.FileName;
                

                foreach (string s in lines)
                {
                    
                    if(s.Length>2)
                    {
                        //if (folderPath.Equals(s.Split(' ')[1]))
                        //{
                        //    testlink = s.Split(' ')[0];
                        //    break;
                        //}
                        if(s.Contains(folderPath))
                        {
                            testlink = s.Substring(0, 4);
                        }

                    }
                    
                  
                }
                if(testlink != "")
                {
                    Lock.IsEnabled = false;
                    Unlock.IsEnabled = true;
                    Open.IsEnabled = true;
                }
                else
                {
                    Lock.IsEnabled = true;
                    Unlock.IsEnabled = false;
                    Open.IsEnabled = false;

                }
                Url.Text = folderPath;
                



            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
