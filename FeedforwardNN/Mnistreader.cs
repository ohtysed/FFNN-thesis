using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;

namespace FeedforwardNN
{
    // https://stackoverflow.com/questions/49407772/reading-mnist-database 
    class Mnistreader
    {
        //This class reads the dataset, it also has some extensions such as viewimage, which displays an image on png file in the map 'images'

        //paths are relative, make sure path is configured to \net6.0 as base resp
        private const string TrainImages = "mnist/train-images.idx3-ubyte";
        private const string TrainLabels = "mnist/train-labels.idx1-ubyte";
        private const string TestImages = "mnist/t10k-images.idx3-ubyte";
        private const string TestLabels = "mnist/t10k-labels.idx1-ubyte";
        /// <summary>
        /// Returns training data
        /// </summary>
        /// <param name="x">My number of training data</param>
        /// <returns>Returns collection of training images</returns>
        public static IEnumerable<Image> ReadTrainingData(int x)
        {
            int y = 0;
            if (x==y)
            {
                yield break;
            }
            foreach (var item in Read(TrainImages, TrainLabels))
            {
                yield return item;
                y += 1;
            }
        }

        /// <summary>
        /// Returns test data 
        /// </summary>
        /// <param name="x">Amount of test data</param>
        /// <returns>Returns collection of test images</returns>
        public static IEnumerable<Image> ReadTestData(int x)
        {

            int y = 0;
            if (x == y)
            {
                yield break;
            }
            foreach (var item in Read(TestImages, TestLabels))
            {
                yield return item;
                y += 1;
            }
        }

        /// <summary>
        /// Method for reading the MNIST train & test dataset
        /// Reading is done by using BinaryReader liberary, 
        /// Reading from images bytefile, the first 4 bytes represent the follow:
        /// 1. magic number 2. number of images 3. width 4. height
        /// Reading from the labels bytefile, the first 2 bytes represent:
        /// 1. magic label 2. number of labels
        /// </summary>
        private static IEnumerable<Image> Read(string imagesPath, string labelsPath)
        {
            // read the labels and images with opening filestream and inputting the output in binaryreader
            BinaryReader labels = new BinaryReader(new FileStream(labelsPath, FileMode.Open));
            BinaryReader images = new BinaryReader(new FileStream(imagesPath, FileMode.Open));


            // read in this sequence to receive information about the following:
            int magicNumber = images.ReadBigInt32(); // if random needed
            int numberOfImages = images.ReadBigInt32();
            int width = images.ReadBigInt32();
            int height = images.ReadBigInt32();

            int magicLabel = labels.ReadBigInt32(); // if random needed
            int numberOfLabels = labels.ReadBigInt32();

            // lastly, read data=values for number images specified before
            for (int i = 0; i < 100; i++) // can specify here how many images
            {
                if (width != 28 || height != 28) continue; //make sure all images are same size or else dont read
                var bytes = images.ReadBytes(width * height); // read 784(28*28)                 

                yield return new Image()
                {
                    Data = bytes,
                    Label = labels.ReadByte(),
                    width = width,
                    height = height,
                };
            }
        }

    }

    public class Image
    {
        public byte Label { get; set; }
        public byte[] Data { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        
    }

    public static class Extensions
    {
        public static void viewimage(byte[] greyscaleValues, int width, int height, int number)
        {
            string outputPath = "mnist/Images";
            int i = 0;

            using (Bitmap bitmap = new Bitmap(width, height))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte value = greyscaleValues[i];
                        Color color = Color.FromArgb(value, value, value);
                        bitmap.SetPixel(x, y, color);
                        i += 1;
                    }
                }
                // Ensure the output directory exists
                System.IO.Directory.CreateDirectory(outputPath);

                string filePath = System.IO.Path.Combine(outputPath, "greyscale_image"+number+".png");
                bitmap.Save(filePath);
            }
        }


        public static int ReadBigInt32(this BinaryReader br)
        {
            var bytes = br.ReadBytes(sizeof(Int32));
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

  
    }
}
