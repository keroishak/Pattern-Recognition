using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pattern_Recognition.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
namespace Pattern_Recognition
{
    public struct Pixel
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public Pixel(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
    internal enum Classifier
    {
        SingleFeatureBayes,
        MultiFeatureBayes

    }
    internal struct RGB
    {
        public int RMeau { get; set; }
        public int RSigma { get; set; }
        public int GMeau { get; set; }
        public int GSigma { get; set; }
        public int BMeau { get; set; }
        public int BSigma { get; set; }
    }
}
namespace Pattern_Recognition.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        Pattern_Recognition.Model.Image img;
        Pixel[] segments = new Pixel[4];
        int classes = 0;
        int[] m = new int[4];
        int[] s = new int[4];
        public MainWindow()
        {
            InitializeComponent();
            segments[0].R = 255;
            segments[1].G = 255;
            segments[2].B = 255;
            segments[3].R = 255;
            segments[3].G = 255;
        }
        void ClassesImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point coo = e.GetPosition(this.ClassesImg);
            if (classes < 4)
            {
                m[classes] = img.GetPixel((uint)coo.X, (uint)coo.Y).R;
                if (classes == 0)
                    Class1Mu.Text = m[classes].ToString();
                else if (classes == 1)
                    Class2Mu.Text = m[classes].ToString();
                else if (classes == 2)
                    Class3Mu.Text = m[classes].ToString();
                else if (classes == 3)
                    Class4Mu.Text = m[classes].ToString();
                ++classes;
            }
        }        

        private void AddMeauNotation(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(sender)).Text == "")
                ((System.Windows.Controls.TextBox)(sender)).Text = "Mu";
        }

        private void RemovenMeauNotation(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(sender)).Text == "Mu")
                ((System.Windows.Controls.TextBox)(sender)).Text = "";
        }

        private void AddSigmaNotation(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(sender)).Text == "")
                ((System.Windows.Controls.TextBox)(sender)).Text = "Sigma";
        }
        private void RemovenSigmaNotation(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(sender)).Text == "Sigma")
                ((System.Windows.Controls.TextBox)(sender)).Text = "";
        }
        List<RGB> Classes = new List<RGB>();
        private void KickoffButton(object sender, RoutedEventArgs e)
        {
            Classes.Clear();
            Regex regex = new Regex("[^0-9.-]+");
            if (!regex.IsMatch(ClassABMeau.Text) && !regex.IsMatch(ClassARMeau.Text) &&
                !regex.IsMatch(ClassAGMeau.Text) &&
                !regex.IsMatch(ClassBBMeau.Text) && !regex.IsMatch(ClassBRMeau.Text) &&
                !regex.IsMatch(ClassBGMeau.Text) &&
                !regex.IsMatch(ClassCBMeau.Text) && !regex.IsMatch(ClassCRMeau.Text) &&
                !regex.IsMatch(ClassCGMeau.Text) &&
                !regex.IsMatch(ClassDBMeau.Text) && !regex.IsMatch(ClassDRMeau.Text) &&
                !regex.IsMatch(ClassDGMeau.Text) &&

                !regex.IsMatch(ClassABSigma.Text) && !regex.IsMatch(ClassARSigma.Text) &&
                !regex.IsMatch(ClassAGSigma.Text) &&
                !regex.IsMatch(ClassBBSigma.Text) && !regex.IsMatch(ClassBRSigma.Text) &&
                !regex.IsMatch(ClassBGSigma.Text) &&
                !regex.IsMatch(ClassCBSigma.Text) && !regex.IsMatch(ClassCRSigma.Text) &&
                !regex.IsMatch(ClassCGSigma.Text) &&
                !regex.IsMatch(ClassDBSigma.Text) && !regex.IsMatch(ClassDRSigma.Text) &&
                !regex.IsMatch(ClassDGSigma.Text) &&
                !regex.IsMatch(Width.Text) && !regex.IsMatch(Height.Text)
                )
            {
                Classes.Add(new RGB { RMeau = int.Parse(ClassARMeau.Text),
                                      RSigma = int.Parse(ClassARSigma.Text),
                                      GMeau = int.Parse(ClassAGMeau.Text),
                                      GSigma = int.Parse(ClassAGSigma.Text),
                                      BMeau = int.Parse(ClassABMeau.Text),
                                      BSigma = int.Parse(ClassABSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = int.Parse(ClassBRMeau.Text),
                    RSigma = int.Parse(ClassBRSigma.Text),
                    GMeau = int.Parse(ClassBGMeau.Text),
                    GSigma = int.Parse(ClassBGSigma.Text),
                    BMeau = int.Parse(ClassBBMeau.Text),
                    BSigma = int.Parse(ClassBBSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = int.Parse(ClassCRMeau.Text),
                    RSigma = int.Parse(ClassCRSigma.Text),
                    GMeau = int.Parse(ClassCGMeau.Text),
                    GSigma = int.Parse(ClassCGSigma.Text),
                    BMeau = int.Parse(ClassCBMeau.Text),
                    BSigma = int.Parse(ClassCBSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = int.Parse(ClassDRMeau.Text),
                    RSigma = int.Parse(ClassDRSigma.Text),
                    GMeau = int.Parse(ClassDGMeau.Text),
                    GSigma = int.Parse(ClassDGSigma.Text),
                    BMeau = int.Parse(ClassDBMeau.Text),
                    BSigma = int.Parse(ClassDBSigma.Text),
                });
                img = TrainModel.GenerateTrainingDataset(ref Classes, uint.Parse(Width.Text), uint.Parse(Height.Text));
                this.ClassesImg.Source =img.Source();
            }
            else
                System.Windows.MessageBox.Show("Make sure that all your inputs are numbers only!");
        }
        private void ClassificationButton(object sender, RoutedEventArgs e)
        {            
            m[0] = (Classes[0].BMeau + Classes[0].GMeau + Classes[0].RMeau) / 3;
            m[1] = (Classes[1].BMeau + Classes[1].GMeau + Classes[1].RMeau) / 3;
            m[2] = (Classes[2].BMeau + Classes[2].GMeau + Classes[2].RMeau) / 3;
            m[3] = (Classes[3].BMeau + Classes[3].GMeau + Classes[3].RMeau) / 3;
            
            s[0] = (Classes[0].BSigma + Classes[0].GSigma + Classes[0].RSigma) / 3;
            s[1] = (Classes[1].BSigma + Classes[1].GSigma + Classes[1].RSigma) / 3;
            s[2] = (Classes[2].BSigma + Classes[2].GSigma + Classes[2].RSigma) / 3;
            s[3] = (Classes[3].BSigma + Classes[3].GSigma + Classes[3].RSigma) / 3;
            img = ScoreModel.Score(Classifier.SingleFeatureBayes, img, ref m, ref s, ref segments);    
            this.ClassesImg.Source = img.Source();
            
        }
        private void GrayScaleButton(object sender, RoutedEventArgs e)
        {
            if(img==null)
            {
                System.Windows.MessageBox.Show("Generate an image first");
                return;
            }
            img.GrayScale();
            this.ClassesImg.Source =img.Source();
        }
        private void LoadButton(object sender, RoutedEventArgs e)
        {
             OpenFileDialog dialog = new OpenFileDialog();
             
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Bitmap loadedfile = new System.Drawing.Bitmap(dialog.FileName);
                img = new Pattern_Recognition.Model.Image(loadedfile);
                this.ClassesImg.Source = img.Source();
                classes = 0;
                this.ClassesImg.MouseLeftButtonDown += ClassesImg_MouseLeftButtonDown;
            }
        }
        private void ClassifyLoadedImageButton(object sender, RoutedEventArgs e)
        {
            if (classes == 4)
            {
                s[0] = int.Parse(Class1Sigma.Text);
                s[1] = int.Parse(Class2Sigma.Text);
                s[2] = int.Parse(Class3Sigma.Text);
                s[3] = int.Parse(Class4Sigma.Text);
                img.GrayScale();
                this.ClassesImg.Source = ScoreModel.Score(Classifier.SingleFeatureBayes, img, ref m, ref s, ref segments).Source();
            }
            else
                System.Windows.MessageBox.Show("select 4 pixel");
        }
    }
}
