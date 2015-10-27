using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Recognition.ViewModel.Classifiers
{
    class SingleFeatureBayesClassifier
    {
        uint NumofClasses;
        double[] priors;
        int[] Sigma;
        int[] Mu;
        public SingleFeatureBayesClassifier(uint classes, ref int[] M, ref int[] S)
        {
            NumofClasses = classes;
            priors = new double[NumofClasses];
            for (int i = 0; i < NumofClasses; ++i)
                priors[i] = 1 / NumofClasses;
                Mu = M;
            Sigma = S;
        }
        double[] Normalize(int x)
        {
            double[] Px_i = new double[NumofClasses];
            for (int i = 0; i < NumofClasses; ++i)
                Px_i[i] = (1 / Math.Sqrt(2 * Math.PI) * Sigma[i]) * Math.Exp(-Math.Pow((x - Mu[i]), 2) / (2 * Math.Pow(Sigma[i], 2)));
            return Px_i;
        }
        public int Classify(int x)
        {
            double[] px_i = Normalize(x);
            double px = 0;
            double[] pi_x = new double[NumofClasses];
            #region Compute Posteriors
            for (int i = 0; i < NumofClasses; ++i)
            {
                pi_x[i] = px_i[i] * priors[i];
                px += pi_x[i];
            }
            for (int i = 0; i < NumofClasses; ++i)
                pi_x[i] /= px;
            #endregion

            // Return the index of the highest probbability of the N classes
            px = 0; int ind = 0;
            for (int i = 0; i < NumofClasses; ++i)
                if (px < pi_x[i])
                {
                    px = pi_x[i];
                    ind = i;
                }
            return ind + 1;
        }
    }
}
