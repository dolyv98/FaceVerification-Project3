using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Security.AccessControl;

namespace CognitiveServices.FaceAPI.Verification
{
    public partial class Capture : Form
    {
        private VideoCaptureDevice cam;
        private FilterInfoCollection cameras;
        public bool rmp;
        private int _code;
        private string _testlink;
        String[] lines = File.ReadAllLines(@"E:\20182\CognitiveServices-Samples-master\assets\list.txt");
        //String[] lines1;
        //String[] lines2;
        //int count = 0;
       

        public Capture()
        {
            InitializeComponent();
            //foreach (string s in lines)
            //{
            //    while(s != null && s!= "")
            //    {
            //        lines1[count] = lines[count].Split(' ')[0];
            //        lines2[count] = lines[count].Split(' ')[1];
            //        count++;
            //    }
            //}
            TakePhoto.Enabled = false;
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo info in cameras)
            {
                Devices.Items.Add(info.Name);
            }
            Devices.SelectedIndex = 0;
        }
        public Capture(int code, string testlink) : this()
        {
            _code = code;
            _testlink = testlink;
        }
        
        private void Start_Click(object sender, EventArgs e)
        {


            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }
            cam = new VideoCaptureDevice(cameras[Devices.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame;
            cam.Start();
            TakePhoto.Enabled = true;
        }

        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            FaceIn.Image = bitmap;
         //   FaceIn.Image = bitmap;
            FaceIn.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }

        }

       

        private void Start_Click_1(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }
            cam = new VideoCaptureDevice(cameras[Devices.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame;
            cam.Start();
            TakePhoto.Enabled = true;

        }

        private void TakePhoto_Click_1(object sender, EventArgs e)
        {
            if(_code == 1)
            {
                // Lock một folder 
                string id;
                Random rd = new Random();
                id = rd.Next(1000, 9999).ToString();
                while(File.Exists(@"E:\20182\CognitiveServices-Samples-master\assets" + id + ".jpg"))
                    {
                    id = rd.Next(1000, 9999).ToString();
                }


                //if(File.Exists(@"E:\20182\CognitiveServices-Samples-master\assets" + id + ".jpg"))
                //    {
                //    File.Delete(@"E:\20182\CognitiveServices-Samples-master\assets" + id + ".jpg");
                //}
                FaceIn.Image.Save(@"E:\20182\CognitiveServices-Samples-master\assets" + id + ".jpg");
                //Lưu vào file

                using (StreamWriter sw = new StreamWriter(@"E:\20182\CognitiveServices-Samples-master\assets\list.txt"))
                {
                    sw.WriteLine(id + " " + _testlink);
                }
                string adminUserName = Environment.UserName;
                 
                    DirectorySecurity ds = Directory.GetAccessControl(_testlink);
                     FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                     ds.AddAccessRule(fsa);
                    Directory.SetAccessControl(_testlink, ds);


            }
            if(_code ==2)
            {
                //unlock
                if(File.Exists(@"E:\20182\tmp.jpg"))
                    {
                    File.Delete(@"E:\20182\tmp.jpg");
                }
                FaceIn.Image.Save(@"E:\20182\tmp.jpg");

                //FaceIn.Image = Image.FromFile(@"E:\20182\tmp.jpg");
                //FaceIn.SizeMode = PictureBoxSizeMode.StretchImage;
                // cam.Stop();
                if (cam != null && cam.IsRunning)
                {
                    cam.Stop();

                }
                MainWindow mw = new MainWindow(_testlink);
                mw.Show();

            }
            
            MessageBox.Show("Took photo and the .png is saved");
            //String tempp = _message + "\\pass.png";
           
            
            this.Close();




        }

        private void Stop_Click_1(object sender, EventArgs e)
        {
            if (cam != null && cam.IsRunning)
            {
                cam.Stop();
            }


        }

        private void FaceIn_Click(object sender, EventArgs e)
        {

        }
    }
}
