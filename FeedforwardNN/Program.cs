using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Collections;
using System.Net.NetworkInformation;

namespace FeedforwardNN
{
    class Program
    {
        static void Main(string[] args)
        {

            Collection<Image> test_data = new Collection<Image>();
            Collection<Image> trainingData = new Collection<Image>();
            foreach (var item in Mnistreader.ReadTrainingData())
            {
                trainingData.Add(item);
                
            }

            foreach (var item in Mnistreader.ReadTestData())
            {
                test_data.Add(item);
            }

            FeedforwardNN network = new FeedforwardNN(trainingData, test_data);
            network.readandtrainpattern();

            Console.WriteLine(network.wrong);


        }
    }
}