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
using System.Windows.Shapes;
using Pattern_Recognition.ViewModel;
using System.Collections.ObjectModel;
namespace Pattern_Recognition.View
{
    /// <summary>
    /// Interaction logic for MultivariateDataWindow.xaml
    /// </summary>
    class ABCD
    {
       public string A { get; set; }
       public string B { get; set; }
       public string C { get; set; }
    }
    public partial class MultivariateDataWindow : Window
    {
        public MultivariateDataWindow()
        {
            InitializeComponent();
        }

        private void TrainButton(object sender, RoutedEventArgs e)
        {
            TrainModel.TrainDataSet(40);
        }

        private void ScoreButton(object sender, RoutedEventArgs e)
        {
           int[,]arr= ScoreModel.ScoreDiscrminantFunction();
           ObservableCollection<ABCD> list = new ObservableCollection<ABCD>();
           for (int i = 0; i < arr.GetLength(0); ++i)
           {
               list.Add(new ABCD());
               
                   list[i].A=arr[i, 0].ToString();
                   list[i].B = arr[i, 1].ToString();
                   list[i].C = arr[i, 2].ToString();
           }
           ConfussionGrid.ItemsSource = list;
            
           OverallAccuracylabel.Visibility = Visibility.Visible;
            double sum=0;
            for (int i = 0; i < arr.GetLength(0); ++i)
                sum += arr[i, i];
            sum=sum/150 *100;
            Accuracylabel.Text = sum.ToString();
        }
    }
}
