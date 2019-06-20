using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Security.AccessControl;




namespace CognitiveServices.FaceAPI.Verification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Face securedFace;
        double imageResizeFactor;
        private string _message;
        private int _status;
      //  private String[] _code;
        private int _status1;
        // string filePath3 = "";
        String[] lines = File.ReadAllLines(@"E:\20182\CognitiveServices-Samples-master\assets\list.txt");
        String[] lines1;
        String[] lines2;
        string _id;




        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient(
            "02d604b79bb041489ceb47aacd7590db",
            "https://westus2.api.cognitive.microsoft.com/face/v1.0");

        public MainWindow()
        {
            InitializeComponent();
            
        }
        public MainWindow(string message) : this()
        {
            _message = message;
          
        }

        public MainWindow(int status1) : this()
        {
            _status1 = status1;
        }


        private async void BrowseButtonIdentification_Click(object sender, RoutedEventArgs e)
        {

            var filePath = @"E:\20182\tmp.jpg";
            //FaceIdentificationPhoto.Source = @"C:\Users\20165\Pictures\Libraries\josh-salacup-1637099-unsplash.jpg";
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();

            bmp.UriSource = new Uri(@"E:\20182\tmp.jpg");
            bmp.EndInit();
            FaceIdentificationPhoto.Source = bmp;

            //Capture c1 = new Capture();
            //c1.Show();

            //    Capture t2 = new 
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }
            if(File.Exists(filePath))
            {
                var detectedFaces = await UploadAndDetectFaces2(filePath, FaceIdentificationPhoto);
                securedFace = detectedFaces[0];

            }
            // Detect any faces in the image.

            foreach (string s in lines)
            {
                if (s.Contains(_message))
                {
                    _id = s.Split(' ')[0];
                }
            }
            string filePath1 = @"E:\20182\CognitiveServices-Samples-master\assets" + _id + ".jpg";

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            // Verify face in the image.
            var result = await VerifyFaces(filePath1, FaceVerificationPhoto);
            if (result == null)
            {
                faceDescriptionStatusBar.Text = "Verification result: No face detected. Please try again.";
                return;
            }

            faceDescriptionStatusBar.Text = $"Verification result: The two faces belong to the same person. Confidence is {result.Confidence}.";
            if ( result.Confidence > 0.5)
            {
                string adminUserName = Environment.UserName;// getting your adminUserName
                DirectorySecurity ds = Directory.GetAccessControl(_message);
                FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);

                ds.RemoveAccessRule(fsa);
                Directory.SetAccessControl(_message, ds);
                //if (File.Exists(filePath1))
                //{
                //    File.Delete(filePath1);
                //}
              //  String lines = File.ReadAllText(@"E:\20182\CognitiveServices-Samples-master\assets\list.txt");
                
                MessageBox.Show("UnLock");
            }


        }

        //private async void BrowseButtonVerification_Click(object sender, RoutedEventArgs e)
        //{
        //    //InitializeComponent();
        //    //foreach (string s in lines)
        //    //{
        //    //    if (s.Contains(_message))
        //    //    {
        //    //        _id = s.Split(' ')[0];
        //    //    }
        //    //}
        //    //string filePath = @"E:\20182\CognitiveServices-Samples-master\assets" + _id + ".jpg";

        //    //if (string.IsNullOrWhiteSpace(filePath))
        //    //{
        //    //    return;
        //    //}

        //    //Verify face in the image.
        //    //var result = await VerifyFaces(filePath, FaceVerificationPhoto);
        //    //if (result == null)
        //    //{
        //    //    faceDescriptionStatusBar.Text = "Verification result: No face detected. Please try again.";
        //    //    return;
        //    //}

        //    //faceDescriptionStatusBar.Text = $"Verification result: The two faces belong to the same person. Confidence is {result.Confidence}.";
        //}

        //private string BrowsePhoto()
        //{
        //    var openDlg = new Microsoft.Win32.OpenFileDialog();

        //    openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";
        //    bool? result = openDlg.ShowDialog(this);

        //    if (!(bool)result)
        //    {
        //        return string.Empty;
        //    }

        //    return openDlg.FileName;
        //}
        private string BrowsePhoto()
        {
            var openDlg = new Microsoft.Win32.OpenFileDialog();

            openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";
            bool? result = openDlg.ShowDialog(this);

            if (!(bool)result)
            {
                return string.Empty;
            }

            return openDlg.FileName;
        }

        private async Task<Face[]> UploadAndDetectFaces2(string imageFilePath, Image image)
        {
            Title = "Detecting...";

            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Gender,
                FaceAttributeType.Age,
                FaceAttributeType.Smile,
                FaceAttributeType.Emotion,
                FaceAttributeType.Glasses,
                FaceAttributeType.Hair
            };

            var faces = new Face[0];

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    faces = await faceServiceClient.DetectAsync(
                        imageFileStream,
                        returnFaceId: true,
                        returnFaceLandmarks: false,
                        returnFaceAttributes: faceAttributes);
                }
            }
            catch (FaceAPIException f)
            {
                MessageBox.Show(f.ErrorMessage, f.ErrorCode);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }

            Title = String.Format("Detection Finished. {0} face(s) detected", faces.Length);

            if (faces.Length > 0)
            {
                Uri fileUri = new Uri(imageFilePath);
                BitmapImage bitmapSource = new BitmapImage();

                bitmapSource.BeginInit();
                bitmapSource.CacheOption = BitmapCacheOption.None;
                bitmapSource.UriSource = fileUri;
                bitmapSource.EndInit();

                image.Source = bitmapSource;

                // Prepare to draw rectangles around the faces.
                DrawingVisual visual = new DrawingVisual();
                DrawingContext drawingContext = visual.RenderOpen();
                drawingContext.DrawImage(bitmapSource, new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
                double dpi = bitmapSource.DpiX;
                imageResizeFactor = 96 / dpi;

                for (int i = 0; i < faces.Length; ++i)
                {
                    Face face = faces[i];

                    Brush genderBrush;
                    if (face.FaceAttributes.Gender == "male")
                        genderBrush = Brushes.Blue;
                    else
                        genderBrush = Brushes.DeepPink;

                    // Draw a rectangle on the face.
                    drawingContext.DrawRectangle(
                        Brushes.Transparent,
                        new Pen(genderBrush, 2),
                        new Rect(
                            face.FaceRectangle.Left * imageResizeFactor,
                            face.FaceRectangle.Top * imageResizeFactor,
                            face.FaceRectangle.Width * imageResizeFactor,
                            face.FaceRectangle.Height * imageResizeFactor
                            )
                    );
                }

                drawingContext.Close();

                // Display the image with the rectangle around the face.
                RenderTargetBitmap faceWithRectBitmap = new RenderTargetBitmap(
                    (int)(bitmapSource.PixelWidth * imageResizeFactor),
                    (int)(bitmapSource.PixelHeight * imageResizeFactor),
                    96,
                    96,
                    PixelFormats.Pbgra32);

                faceWithRectBitmap.Render(visual);
                image.Source = faceWithRectBitmap;
            }

            return faces;
        }

        private async Task<VerifyResult> VerifyFaces(string imageFilePath, Image image)
        {
            Title = "Verifying...";
            var verificationFaces = await UploadAndDetectFaces2(imageFilePath, image);

            if (verificationFaces.Length == 0)
            {
                return null;
            }

            return await faceServiceClient.VerifyAsync(verificationFaces[0].FaceId, securedFace.FaceId);
        }

        
    }
}
