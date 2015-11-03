using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pattern_Recognition.ViewModel.Classifiers;
using Pattern_Recognition.Model;
namespace Pattern_Recognition.ViewModel
{
    static class ScoreModel
    {
        public static Image Score(Classifier c, Image img, ref int[] M, ref int[] S, ref Pixel[] segments)
        {
            IClassifier classifier = null;
            Image Img = new Image(img.Width, img.Height, img.Components);
            if (c == Classifier.SingleFeatureBayes)
                classifier = new SingleFeatureBayesClassifier(ref M, ref S);
            int WorkingClass;
            for (uint w = 0; w < img.Width; ++w)
            {
                for (uint h = 0; h < img.Height; ++h)
                {
                    WorkingClass = classifier.Classify(img.GetPixel(w, h).R);
                    Img.SetPixel(w, h, ref segments[WorkingClass]);
                }
            }
            return Img;
        }
       
    }
}
