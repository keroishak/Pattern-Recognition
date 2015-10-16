using Pattern_Recognition.View;
//This Class 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Pattern_Recognition.Model;
namespace Pattern_Recognition.ViewModel
{
   
    class TrainedDataGeneration :INotifyPropertyChanged
    {
       
        private uint width { get; set; }
        private uint height { get; set; }
        private Image img;
        private uint NumofClasses;

        private double NormalRandom(double mu, double sigma)
        {
            Random r = new Random();
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            var rand_normal = mu + sigma * rand_std_normal;

            return rand_normal;
        }
        public void GenerateRandominzedColoredImage(ref List<RGB>Classes, uint height, uint width)
        {
            NumofClasses = 4;
            Random Rand = new Random();
            this.width = width;
            this.height = height;
            double generatednumber;
            img= new Image(width,height);
            Pixel P=new Pixel();
            uint Workingsegment = width / NumofClasses;
            int WorkingClass = 0;
                for (uint w = 0; w < width; ++w)
                {
                    if(w==Workingsegment)
                    {
                        ++WorkingClass;
                        Workingsegment += width / NumofClasses;
                    }
                    for (uint h = 0; h < height; ++h)
                    {
                        generatednumber = Rand.NextDouble();
                        //P.R = (byte)(Classes[WorkingClass].RMeau + generatednumber * Classes[WorkingClass].RSigma);
                        P.R = (byte) Math.Abs(NormalRandom(Classes[WorkingClass].RMeau, Classes[WorkingClass].RSigma));
                        generatednumber = Rand.NextDouble();
                        //P.G = (byte)(Classes[WorkingClass].GMeau + generatednumber * Classes[WorkingClass].GSigma);
                        P.G = (byte)Math.Abs(NormalRandom(Classes[WorkingClass].GMeau, Classes[WorkingClass].GSigma));
                        generatednumber = Rand.NextDouble();
                        //P.B = (byte)(Classes[WorkingClass].BMeau + generatednumber * Classes[WorkingClass].BSigma);
                        P.B = (byte)Math.Abs(NormalRandom(Classes[WorkingClass].BMeau, Classes[WorkingClass].BSigma));
                        img.SetPixel(w,h,ref P);
                    }
                }
            SourceImage = img.Source();
        }

        public event PropertyChangedEventHandler PropertyChanged;
       
        private BitmapSource _SourceImage;
        public  BitmapSource SourceImage
        {
            
            get { 
                return _SourceImage; }
            set 
            { 
                _SourceImage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this,new PropertyChangedEventArgs("MainImage"));
            }
            
        }
    }
}
