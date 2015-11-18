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
        public static int[,] ConfusionMat = new int[5, 5];
        public static Image ScoreSingleFeature(Image img, ref int[] M, ref int[] S, ref Pixel[] segments)
        {
            Image Img = new Image(img.Width, img.Height, img.Components);
            SingleFeatureBayesianClassifier classifier = new SingleFeatureBayesianClassifier(ref M, ref S);
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
        public static Image ScoreMultiFeature(Image img, ref int[,] M, ref int[,] S, ref Pixel[] segments, ref double[,] loosmatrix)
        {
            Image Img = new Image(img.Width, img.Height, img.Components);
            MultiFeatureBayesianClassifier classifier = new MultiFeatureBayesianClassifier(ref M, ref S);
            int WorkingClass = 0;
            Pixel p;

            for (uint w = 0; w < img.Width; ++w)
            {
                for (uint h = 0; h < img.Height; ++h)
                {
                    p = img.GetPixel(w, h);
                    WorkingClass = classifier.Classify(ref p, ref loosmatrix);
                    Img.SetPixel(w, h, ref segments[WorkingClass]);
                    ++ConfusionMat[img.Class(w), WorkingClass];
                }
            }
            return Img;
        }
        public static int[,] ScoreDiscrminantFunction()
        {
            int classes = TrainModel.dataset.Keys.Count;
            int features = TrainModel.dimension;
            int[,] confussionMat = new int[classes, classes];
            int estimatedclass;
            DiscriminantBayesianClassifier DBclassifier = new DiscriminantBayesianClassifier(TrainModel.mu);
            for (int i = 0; i < classes; ++i)
            {
                for (int j = 0; j < TrainModel.dataset[TrainModel.classes[i]].Count; ++j)
                {
                    estimatedclass = DBclassifier.Classify(TrainModel.dataset[TrainModel.classes[i]][j]);
                    ++confussionMat[i, estimatedclass];
                }
            }
            return confussionMat;
        }
    }
}
