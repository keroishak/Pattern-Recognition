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
using Pattern_Recognition.ViewModel.Dataset;
namespace Pattern_Recognition.ViewModel
{
   
    class TrainModel 
    {        
        public static Image GenerateTrainingDataset(ref List<RGB> Classes, uint height, uint width)
        {
            GeneratedImageDataset img = new GeneratedImageDataset();
            return img.Generate(ref Classes, height, width);
        }
    }
}
