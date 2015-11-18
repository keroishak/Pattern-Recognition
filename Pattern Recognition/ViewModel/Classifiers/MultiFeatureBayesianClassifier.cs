using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Recognition.ViewModel.Classifiers
{
    class MultiFeatureBayesianClassifier
    {
        int NumofFeatures, NumofActions, NumofClasses;
        double PClasses;
        int[,] Mu; int[,] Sigma;
        public MultiFeatureBayesianClassifier(ref int[,] mus, ref int[,] sigmas)
        {
            NumofActions = 5;
            NumofFeatures = mus.GetLength(1);
            NumofClasses = mus.GetLength(0);
            Mu = mus;
            Sigma = sigmas;
            PClasses = (double)1 / NumofClasses;
        }
        double[,] Normalize(ref Pixel x)
        {
            //Gaussian distribution
            double[,] Px_i = new double[NumofClasses, NumofFeatures];
            for (int i = 0; i < NumofClasses; ++i)
            {
                Px_i[i, 0] = ((double)1 / Math.Sqrt(2 * Math.PI) * Sigma[i,0]) * Math.Exp(-Math.Pow((x.R - Mu[i,0]), 2) / (2 * Math.Pow(Sigma[i,0], 2)));
                Px_i[i, 1] = ((double)1 / Math.Sqrt(2 * Math.PI) * Sigma[i,1]) * Math.Exp(-Math.Pow((x.G - Mu[i,1]), 2) / (2 * Math.Pow(Sigma[i,1], 2)));
                Px_i[i, 2] = ((double)1 / Math.Sqrt(2 * Math.PI) * Sigma[i,2]) * Math.Exp(-Math.Pow((x.B - Mu[i,2]), 2) / (2 * Math.Pow(Sigma[i,2], 2)));
            }
            return Px_i;
        }
        public int Classify(ref Pixel features, ref double[,] loss)
        {

            double[,] pxjwi = Normalize(ref features);
            double[] pxwi = new double[NumofClasses];
            for (int i = 0; i < NumofClasses; ++i)
            {
                pxwi[i] = 1;
                for (int j = 0; j < NumofFeatures; ++j)
                    pxwi[i] *= pxjwi[i, j];
            }
            double sum = 0;
            for (int i = 0; i < NumofClasses; ++i)
            {
                pxwi[i] *= PClasses;
                sum += pxwi[i];
            }
            for (int i = 0; i < NumofClasses; ++i)
            {
                pxwi[i] /= sum;
            }
            double[] r = new double[NumofActions];

            for (int i = 0; i < NumofActions; ++i)
            {
                sum = 0;
                for (int j = 0; j < NumofClasses; ++j)
                    sum += loss[i,j] * pxwi[j];
                r[i] = sum;
            }
            // Return the index of the lowest risk of the N Actions
            double x = int.MaxValue; int ind = 0;
            for (int i = 0; i < NumofActions; ++i)
                if (x > r[i])
                {
                    x = r[i];
                    ind = i;
                }
            return ind;
        }
    }
}
