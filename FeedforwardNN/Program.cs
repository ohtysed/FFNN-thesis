using System.Reflection.Metadata;

namespace FeedforwardNN
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 0;
            foreach (var image in Mnistreader.ReadTrainingData(10))
            {
            
                Console.WriteLine("start");
                Extensions.viewimage(image.Data, image.width,image.height, number);
                Console.WriteLine("end");
                number += 1;
            }
        }
    }
}