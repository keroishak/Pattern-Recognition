using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Recognition.ViewModel.Classifiers
{
    interface IClassifier
    {
         int Classify(int x);
    }
}
