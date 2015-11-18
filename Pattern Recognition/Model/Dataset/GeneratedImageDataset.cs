using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pattern_Recognition.Model;
namespace Pattern_Recognition.Model.Dataset
{
    class GeneratedImageDataset
    {
        Random Rand;
        public GeneratedImageDataset()
        {
            Rand= new Random();
        }
        byte BoundaryCheck(double X)
        {
            if (X > 255.0)
                return 255;
            else if (X < 0)
                return 0;
            return (byte)X;
        }
        double GetNextRandom()
        {           
            return Math.Sqrt(-2.0 * Math.Log10(Rand.NextDouble())) * Math.Cos(Math.PI * Rand.NextDouble());          
        }
        public Image Generate(ref List<RGB> Classes, uint height, uint width)
        {
            uint NumofClasses = (uint)Classes.Count;
            Image img = new Image(width, height, 3, 4);
            Pixel P = new Pixel();
            uint Workingsegment = width / NumofClasses;
            int WorkingClass = 0;
            for (uint w = 0; w < width; ++w)
            {
                if (w == Workingsegment && WorkingClass < NumofClasses - 1)
                {
                    ++WorkingClass;
                    Workingsegment += width / NumofClasses;
                    img.AddClass(w);
                }
                for (uint h = 0; h < height; ++h)
                {

                    P.R = BoundaryCheck(Classes[WorkingClass].RMeau + GetNextRandom() * Classes[WorkingClass].RSigma);

                    P.G = BoundaryCheck(Classes[WorkingClass].GMeau + GetNextRandom() * Classes[WorkingClass].GSigma);

                    P.B = BoundaryCheck(Classes[WorkingClass].BMeau + GetNextRandom() * Classes[WorkingClass].BSigma);
                    img.SetPixel(w, h, ref P);
                }
            }

            return img;

        }
    }
}
