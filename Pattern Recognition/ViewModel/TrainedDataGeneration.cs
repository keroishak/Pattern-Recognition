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
        Random Rand;
        const double two_pi = 2.0 * 3.14159265358979323846;
        public TrainedDataGeneration()
        {
            Rand= new Random();
        }
        public byte BoundaryCheck(double X)
        {
            if (X > 255.0)
                return 255;
            else if (X < 0)
                return 0;
            return (byte)X;
        }
        public double GetNextRandom()
        {

            return Math.Sqrt(-2.0 * Math.Log10(Rand.NextDouble())) * Math.Cos(two_pi * Rand.NextDouble());
           
        }
        public void GenerateRandominzedColoredImage(ref List<RGB>Classes, uint height, uint width)
        {
            NumofClasses = 4;
            
            this.width = width;
            this.height = height;
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
                        
                        P.R = BoundaryCheck(Classes[WorkingClass].RMeau + GetNextRandom() * Classes[WorkingClass].RSigma);

                        P.G = BoundaryCheck(Classes[WorkingClass].GMeau + GetNextRandom() * Classes[WorkingClass].GSigma);

                        P.B = BoundaryCheck(Classes[WorkingClass].BMeau + GetNextRandom() * Classes[WorkingClass].BSigma);
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
