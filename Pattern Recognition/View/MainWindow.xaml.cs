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
using System.Collections.ObjectModel;
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
    public class ABC
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double Black { get; set; }
        public ABC()
        {

        }
    }
    public partial class MainWindow : Window
    {
        Pattern_Recognition.Model.Image img;
        Pixel[] segments = new Pixel[5];
        int classes = 0;
        int[] m = new int[4];
        int[] s = new int[4];
        ObservableCollection<ABC> grid = new ObservableCollection<ABC>();
        public ObservableCollection<ABC> LoosGrid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            segments[0].R = 255;
            segments[1].G = 255;
            segments[2].B = 255;
            segments[3].R = 255;
            segments[3].G = 255;
            this.ClassesImg.MouseLeftButtonDown += ClassesImg_MouseLeftButtonDown;
            ObservableCollection<ABC> a = new ObservableCollection<ABC>();
            for (int i = 0; i <= 5; ++i)
                a.Add(new ABC());
            LoosGrid = a;
            this.LoosMatrix.ItemsSource = LoosGrid;
        }
        void ClassesImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            System.Windows.Point coo = e.GetPosition(this.ClassesImg);
            Pixel p;
            if (classes < 4)
            {
                p = img.GetPixel((uint)coo.X, (uint)coo.Y);
                m[classes] = p.R;
                if (Combo.SelectedIndex == 0)
                {
                    if (classes == 0)
                        Class1Mu.Text = m[classes].ToString();
                    else if (classes == 1)
                        Class2Mu.Text = m[classes].ToString();
                    else if (classes == 2)
                        Class3Mu.Text = m[classes].ToString();
                    else if (classes == 3)
                        Class4Mu.Text = m[classes].ToString();
                }
                else if (Combo.SelectedIndex == 1)
                {

                    if (classes == 0)
                    {
                        ClassARMeau.Text = p.R.ToString();
                        ClassAGMeau.Text = p.G.ToString();
                        ClassABMeau.Text = p.B.ToString();
                    }
                    else if (classes == 1)
                    {
                        ClassBRMeau.Text = p.R.ToString();
                        ClassBGMeau.Text = p.G.ToString();
                        ClassBBMeau.Text = p.B.ToString();
                    }
                    else if (classes == 2)
                    {
                        ClassCRMeau.Text = p.R.ToString();
                        ClassCGMeau.Text = p.G.ToString();
                        ClassCBMeau.Text = p.B.ToString();
                    }
                    else if (classes == 3)
                    {
                        ClassDRMeau.Text = p.R.ToString();
                        ClassDGMeau.Text = p.G.ToString();
                        ClassDBMeau.Text = p.B.ToString();
                    }
                }
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
                Classes.Add(new RGB
                {
                    RMeau = int.Parse(ClassARMeau.Text),
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
                this.ClassesImg.Source = img.Source();
            }
            else
                System.Windows.MessageBox.Show("Make sure that all your inputs are numbers only!");
        }
        private void ClassificationButton(object sender, RoutedEventArgs e)
        {
            if (Combo.SelectedIndex == 0)
            {
                m[0] = (Classes[0].BMeau + Classes[0].GMeau + Classes[0].RMeau) / 3;
                m[1] = (Classes[1].BMeau + Classes[1].GMeau + Classes[1].RMeau) / 3;
                m[2] = (Classes[2].BMeau + Classes[2].GMeau + Classes[2].RMeau) / 3;
                m[3] = (Classes[3].BMeau + Classes[3].GMeau + Classes[3].RMeau) / 3;

                s[0] = (Classes[0].BSigma + Classes[0].GSigma + Classes[0].RSigma) / 3;
                s[1] = (Classes[1].BSigma + Classes[1].GSigma + Classes[1].RSigma) / 3;
                s[2] = (Classes[2].BSigma + Classes[2].GSigma + Classes[2].RSigma) / 3;
                s[3] = (Classes[3].BSigma + Classes[3].GSigma + Classes[3].RSigma) / 3;
                img = ScoreModel.ScoreSingleFeature(img, ref m, ref s, ref segments);
                this.ClassesImg.Source = img.Source();
            }
            else if (Combo.SelectedIndex == 1)
            {
                int[,] mus = new int[4, 3];
                int[,] sigmas = new int[4, 3];
                mus[0, 0] = int.Parse(ClassARMeau.Text);
                mus[0, 1] = int.Parse(ClassAGMeau.Text);
                mus[0, 2] = int.Parse(ClassABMeau.Text);
                mus[1, 0] = int.Parse(ClassBRMeau.Text);
                mus[1, 1] = int.Parse(ClassBGMeau.Text);
                mus[1, 2] = int.Parse(ClassBBMeau.Text);
                mus[2, 0] = int.Parse(ClassCRMeau.Text);
                mus[2, 1] = int.Parse(ClassCGMeau.Text);
                mus[2, 2] = int.Parse(ClassCBMeau.Text);
                mus[3, 0] = int.Parse(ClassDRMeau.Text);
                mus[3, 1] = int.Parse(ClassDGMeau.Text);
                mus[3, 2] = int.Parse(ClassDBMeau.Text);
                sigmas[0, 0] = int.Parse(ClassARSigma.Text);
                sigmas[0, 1] = int.Parse(ClassAGSigma.Text);
                sigmas[0, 2] = int.Parse(ClassABSigma.Text);
                sigmas[1, 0] = int.Parse(ClassBRSigma.Text);
                sigmas[1, 1] = int.Parse(ClassBGSigma.Text);
                sigmas[1, 2] = int.Parse(ClassBBSigma.Text);
                sigmas[2, 0] = int.Parse(ClassCRSigma.Text);
                sigmas[2, 1] = int.Parse(ClassCGSigma.Text);
                sigmas[2, 2] = int.Parse(ClassCBSigma.Text);
                sigmas[3, 0] = int.Parse(ClassDRSigma.Text);
                sigmas[3, 1] = int.Parse(ClassDGSigma.Text);
                sigmas[3, 2] = int.Parse(ClassDBSigma.Text);

                double[,] loosmat = new double[5, 4];
                for (int i = 0; i < 5; ++i)
                {

                    loosmat[i, 0] = LoosGrid[i + 1].A;
                    loosmat[i, 1] = LoosGrid[i + 1].B;
                    loosmat[i, 2] = LoosGrid[i + 1].C;
                    loosmat[i, 3] = LoosGrid[i + 1].D;
                }
                this.ClassesImg.Source = ScoreModel.ScoreMultiFeature(img, ref mus, ref sigmas, ref segments, ref loosmat).Source();
                ObservableCollection<ABC> grid = new ObservableCollection<ABC>();
                for (int i = 0; i < 4; ++i)
                {
                    grid.Add(new ABC());
                    grid[i].A = ScoreModel.ConfusionMat[i, 0];
                    grid[i].B = ScoreModel.ConfusionMat[i, 1];
                    grid[i].C = ScoreModel.ConfusionMat[i, 2];
                    grid[i].D = ScoreModel.ConfusionMat[i, 3];
                    grid[i].Black = ScoreModel.ConfusionMat[i, 4];
                }
                double x = ScoreModel.ConfusionMat[0, 0] + ScoreModel.ConfusionMat[1, 1] + ScoreModel.ConfusionMat[2, 2] + ScoreModel.ConfusionMat[3, 3]; ;
                x /= img.Width * img.Height;
                x *= 100;
                this.ConfussionGrid.ItemsSource = grid;
                this.AccuracyTextBlock.Visibility = Visibility.Visible;
                this.AccuracyTextBlock.Text ="Accuracy :  "+ x.ToString() + " %";
                //= img.Source();
            }

        }
        private void GrayScaleButton(object sender, RoutedEventArgs e)
        {
            if (img == null)
            {
                System.Windows.MessageBox.Show("Generate an image first");
                return;
            }
            img.GrayScale();
            this.ClassesImg.Source = img.Source();
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

            }
        }
        private void ClassifyLoadedImageButton(object sender, RoutedEventArgs e)
        {
            if (classes == 4)
            {
                if (Combo.SelectedIndex == 0)
                {
                    s[0] = int.Parse(Class1Sigma.Text);
                    s[1] = int.Parse(Class2Sigma.Text);
                    s[2] = int.Parse(Class3Sigma.Text);
                    s[3] = int.Parse(Class4Sigma.Text);
                    img.GrayScale();
                    this.ClassesImg.Source = ScoreModel.ScoreSingleFeature(img, ref m, ref s, ref segments).Source();
                }
                else if (Combo.SelectedIndex == 1)
                {
                    int[,] mus = new int[4, 3];
                    int[,] sigmas = new int[4, 3];
                    mus[0, 0] = int.Parse(ClassARMeau.Text);
                    mus[0, 1] = int.Parse(ClassAGMeau.Text);
                    mus[0, 2] = int.Parse(ClassABMeau.Text);
                    mus[1, 0] = int.Parse(ClassBRMeau.Text);
                    mus[1, 1] = int.Parse(ClassBGMeau.Text);
                    mus[1, 2] = int.Parse(ClassBBMeau.Text);
                    mus[2, 0] = int.Parse(ClassCRMeau.Text);
                    mus[2, 1] = int.Parse(ClassCGMeau.Text);
                    mus[2, 2] = int.Parse(ClassCBMeau.Text);
                    mus[3, 0] = int.Parse(ClassDRMeau.Text);
                    mus[3, 1] = int.Parse(ClassDGMeau.Text);
                    mus[3, 2] = int.Parse(ClassDBMeau.Text);
                    sigmas[0, 0] = int.Parse(ClassARSigma.Text);
                    sigmas[0, 1] = int.Parse(ClassAGSigma.Text);
                    sigmas[0, 2] = int.Parse(ClassABSigma.Text);
                    sigmas[1, 0] = int.Parse(ClassBRSigma.Text);
                    sigmas[1, 1] = int.Parse(ClassBGSigma.Text);
                    sigmas[1, 2] = int.Parse(ClassBBSigma.Text);
                    sigmas[2, 0] = int.Parse(ClassCRSigma.Text);
                    sigmas[2, 1] = int.Parse(ClassCGSigma.Text);
                    sigmas[2, 2] = int.Parse(ClassCBSigma.Text);
                    sigmas[3, 0] = int.Parse(ClassDRSigma.Text);
                    sigmas[3, 1] = int.Parse(ClassDGSigma.Text);
                    sigmas[3, 2] = int.Parse(ClassDBSigma.Text);
                    double[,] loosmat = new double[5, 4];
                    for (int i = 0; i < 5; ++i)
                    {

                        loosmat[i, 0] = LoosGrid[i + 1].A;
                        loosmat[i, 1] = LoosGrid[i + 1].B;
                        loosmat[i, 2] = LoosGrid[i + 1].C;
                        loosmat[i, 3] = LoosGrid[i + 1].D;
                    }
                    this.ClassesImg.Source = ScoreModel.ScoreMultiFeature(img, ref mus, ref sigmas, ref segments, ref loosmat).Source();

                }
            }
            else
                System.Windows.MessageBox.Show("select 4 pixel");
        }
        private void SingleBSelected(object sender, RoutedEventArgs e)
        {
            this.AccuracyTextBlock.Visibility = Visibility.Collapsed;
            this.LoosMatrix.Visibility = Visibility.Collapsed;
            this.GrayButton.Visibility = Visibility.Visible;
            this.ClassABSigma.Visibility = Visibility.Visible;
            this.ClassAGSigma.Visibility = Visibility.Visible;
            this.ClassBBSigma.Visibility = Visibility.Visible;
            this.ClassBGSigma.Visibility = Visibility.Visible;
            this.ClassCBSigma.Visibility = Visibility.Visible;
            this.ClassCGSigma.Visibility = Visibility.Visible;
            this.ClassDBSigma.Visibility = Visibility.Visible;
            this.ClassDGSigma.Visibility = Visibility.Visible;
        }
        private void MultiBSelected(object sender, RoutedEventArgs e)
        {
            this.LoosMatrix.Visibility = Visibility.Visible;
            this.GrayButton.Visibility = Visibility.Collapsed;
            // this.ClassABSigma.Visibility = Visibility.Collapsed;
            // this.ClassAGSigma.Visibility = Visibility.Collapsed;
            // this.ClassBBSigma.Visibility = Visibility.Collapsed;
            // this.ClassBGSigma.Visibility = Visibility.Collapsed;
            // this.ClassCBSigma.Visibility = Visibility.Collapsed;
            // this.ClassCGSigma.Visibility = Visibility.Collapsed;
            // this.ClassDBSigma.Visibility = Visibility.Collapsed;
            // this.ClassDGSigma.Visibility = Visibility.Collapsed;
        }
    }
}
