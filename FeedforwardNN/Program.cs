using System.Reflection.Metadata;

namespace FeedforwardNN
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in Mnistreader.ReadTrainingData())
            {
                foreach (var pixel in item.Data)
                {
                    Console.WriteLine( pixel);
                }
            }



        }
    }
}