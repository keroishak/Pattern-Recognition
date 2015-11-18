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
using Pattern_Recognition.Model.Dataset;
using System.IO;
using System.Collections;
namespace Pattern_Recognition.ViewModel
{
    class TrainModel
    {
        static public Dictionary<string, List<List<double>>> dataset = new Dictionary<string, List<List<double>>>();
        static public Matrix mu;
        static public List<Matrix> sigma;
        static public string[] classes;
        static public int dimension;
        public static Image GenerateTrainingDataset(ref List<RGB> Classes, uint height, uint width)
        {
            GeneratedImageDataset img = new GeneratedImageDataset();
            return img.Generate(ref Classes, height, width);
        }
        public static void TrainDataSet(int percentage)
        {
            
            FileStream file = new FileStream(@"../../Model/Dataset/Iris Data.txt", FileMode.Open);
            StreamReader Reader = new StreamReader(file);
            string s;
            string[] splitted = Reader.ReadLine().Split(',');
             dimension = splitted.Length - 1;
           // List<double> temp = new List<double>();
            int N = 0;
            int Class = 0;
            classes= new string[3];
            dataset.Clear();
            while (!Reader.EndOfStream)
            {
                List<double> temp = new List<double>();
                s = Reader.ReadLine();
                splitted = s.Split(',');
                for (int i = 0; i < splitted.Length - 1; ++i)
                    temp.Add(double.Parse(splitted[i]));
                if (!dataset.ContainsKey(splitted[splitted.Length - 1]))
                {
                    classes[Class] = splitted[splitted.Length - 1];
                    ++Class;
                    dataset.Add(splitted[splitted.Length - 1], new List<List<double>>());
                }
                dataset[splitted[splitted.Length - 1]].Add(temp);
                ++N;

            }
            sigma = new List<Matrix>();
            mu = new Matrix(dimension,Class);
            percentage = (int)(((double)percentage/100) * dataset[splitted[splitted.Length - 1]].Count);

            for (int c = 0; c < Class; ++c)
            {
                for (int j = 0; j < dimension; ++j)
                {
                    for (int i = 0; i < percentage; ++i)
                    {

                        mu[j,c] += dataset[classes[c]][i][j];
                    }
                    mu[j,c] /= percentage;
                }
            }

            for (int c = 0; c < Class; ++c)
            {
                sigma.Add(new Matrix(dimension, dimension));
                for (int j = 0; j < dimension; ++j)
                {
                    for (int l = 0; l < dimension; ++l)
                    {
                        for (int i = 0; i < percentage; ++i)
                        {

                            sigma[c][j, l] += (mu[j,c] - dataset[classes[c]][i][j]) * ((mu[l,c] - dataset[classes[c]][i][l]));
                        }
                        sigma[c][j, l] /= percentage;
                    }
                }
            } 

        }
    }
}
