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
namespace Pattern_Recognition.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    internal struct RGB
    {
        public double RMeau { get; set; }
        public double RSigma { get; set; }
        public double GMeau { get; set; }
        public double GSigma { get; set; }
        public double BMeau { get; set; }
        public double BSigma { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TrainedDataGeneration();
        }

        private void AddMeauNotation(object sender, RoutedEventArgs e)
        {
            if (((TextBox)(sender)).Text == "")
                ((TextBox)(sender)).Text = "Mu";
        }

        private void RemovenMeauNotation(object sender, RoutedEventArgs e)
        {
            if (((TextBox)(sender)).Text == "Mu")
                ((TextBox)(sender)).Text = "";
        }

        private void AddSigmaNotation(object sender, RoutedEventArgs e)
        {
            if (((TextBox)(sender)).Text == "")
                ((TextBox)(sender)).Text = "Sigma";
        }

        private void RemovenSigmaNotation(object sender, RoutedEventArgs e)
        {
            if (((TextBox)(sender)).Text == "Sigma")
                ((TextBox)(sender)).Text = "";
        }

        private void KickoffButton(object sender, RoutedEventArgs e)
        {
            List<RGB> Classes = new List<RGB>();
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
                Classes.Add(new RGB { RMeau = double.Parse(ClassARMeau.Text),
                                      RSigma = double.Parse(ClassARSigma.Text),
                                      GMeau = double.Parse(ClassAGMeau.Text),
                                      GSigma = double.Parse(ClassAGSigma.Text),
                                      BMeau = double.Parse(ClassABMeau.Text),
                                      BSigma = double.Parse(ClassABSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = double.Parse(ClassBRMeau.Text),
                    RSigma = double.Parse(ClassBRSigma.Text),
                    GMeau = double.Parse(ClassBGMeau.Text),
                    GSigma = double.Parse(ClassBGSigma.Text),
                    BMeau = double.Parse(ClassBBMeau.Text),
                    BSigma = double.Parse(ClassBBSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = double.Parse(ClassCRMeau.Text),
                    RSigma = double.Parse(ClassCRSigma.Text),
                    GMeau = double.Parse(ClassCGMeau.Text),
                    GSigma = double.Parse(ClassCGSigma.Text),
                    BMeau = double.Parse(ClassCBMeau.Text),
                    BSigma = double.Parse(ClassCBSigma.Text),
                });
                Classes.Add(new RGB
                {
                    RMeau = double.Parse(ClassDRMeau.Text),
                    RSigma = double.Parse(ClassDRSigma.Text),
                    GMeau = double.Parse(ClassDGMeau.Text),
                    GSigma = double.Parse(ClassDGSigma.Text),
                    BMeau = double.Parse(ClassDBMeau.Text),
                    BSigma = double.Parse(ClassDBSigma.Text),
                });
                ((TrainedDataGeneration)(this.DataContext)).GenerateRandominzedColoredImage(ref Classes, uint.Parse(Height.Text),uint.Parse( Width.Text));
            }
            else
                MessageBox.Show("Make sure that all your inputs are numbers only!");


        }
    }
}
