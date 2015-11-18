using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pattern_Recognition.Model;
namespace Pattern_Recognition.ViewModel.Classifiers
{
    class DiscriminantBayesianClassifier
    {
        Matrix mu;
        int features,NumofClasses;
        double prior;
        public DiscriminantBayesianClassifier(Matrix m)
        {
            mu = m;
            NumofClasses = TrainModel.classes.Length;
            features = mu.rows;
            prior = 1.0 / NumofClasses;
        }
        public int Classify(List<double> X)
        {
            int index=0; double greaterProp = double.MinValue,value;
            for (int i = 0; i < NumofClasses; ++i)
            {
                Matrix XMU = new Matrix(features, 1);

                    for (int j = 0; j < features; ++j)
                        XMU[j, 0] = X[j] - mu[j, i];

                Matrix transXMU = Matrix.Transpose(XMU);
                transXMU = -0.5 * transXMU;
                Matrix invertedsigma = TrainModel.sigma[i].Invert();
                transXMU = transXMU * invertedsigma * XMU;
                value = transXMU[0, 0] + Math.Log(prior,Math.E);
                if (value > greaterProp)
                {
                    greaterProp = value;
                    index = i;
                }
                // Matrix lnsigma = Math.Log(prior) + (-0.5 * Matrix.ln(TrainModel.sigma[workingclass]));
                //lnsigma = transXMU - lnsigma;
            }
            return index;
        }
    }
}
