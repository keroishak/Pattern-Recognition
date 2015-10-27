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
using Pattern_Recognition.ViewModel.Classifiers;
namespace Pattern_Recognition.ViewModel
{
   
    class TrainedDataGeneration :INotifyPropertyChanged
    {
       
        private uint width { get; set; }
        private uint height { get; set; }
        private Image img;
        private Image grayimg;
        private Image classifiedimg;
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
        public BitmapSource GenerateRandominzedColoredImage(ref List<RGB>Classes, uint height, uint width)
        {
            NumofClasses = 4;
            
            this.width = width;
            this.height = height;
            img= new Image(width,height);
            grayimg=new Image(width,height);
            classifiedimg = new Image(width, height);
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
                
                return img.Source();

        }
        public BitmapSource GrayScale()
        {
            int x; Pixel p;
            for(uint i=0;i<img.Width;++i)
            {
                for(uint j=0;j<img.Height;++j)
                {
                    p = img.GetPixel(i, j);
                    x = (p.B + p.G + p.R) / 3;
                    p.R = (byte)x;
                    p.G = p.R;
                    p.G = p.R;
                    grayimg.SetPixel(i, j,ref p);
                }
            }
            return grayimg.Source();
        }
        public BitmapSource ClassifyTrainedImage(int[]M,int[]S)
        {
            int WorkingClass = 0;
            Pixel[] segments = new Pixel[NumofClasses];
            segments[0].R = 255;
            segments[1].G = 255;
            segments[2].B = 255;
            segments[3].R = 255;
            segments[3].G = 255;
            SingleFeatureBayesClassifier c = new SingleFeatureBayesClassifier(NumofClasses, ref M, ref S);
            for (uint w = 0; w < width; ++w)
            {
                for (uint h = 0; h < height; ++h)
                {
                    WorkingClass = c.Classify(img.GetPixel(w, h).R);
                    classifiedimg.SetPixel(w, h, ref segments[WorkingClass]);
                }
            }
            return classifiedimg.Source();
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
                //if (PropertyChanged != null)
                    PropertyChanged(this,new PropertyChangedEventArgs("MainImage"));
            }
            
        }
    }
}
