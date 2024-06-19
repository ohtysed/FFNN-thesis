using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
        // Class input layer represents the input of the pixels
        // Each neural network will receive a single image, thus intputlayer will receive a stream of bytes of a single image
        // original dataset had binary images, but we use now real numbers from 0-1
        // Todo change to -1 - 1 

        class setinputlayer
        {
     
            public double expect;
            public double[] inputs;
        public double experiment;
            

            public void inputlayer(byte[] Input)
            {
                inputs = new double[Input.Length];
            for (int i = 0; i < Input.Length; i++)
            {
                inputs[i] = linearmapping(Input[i]);
                experiment += Input[i];
                }
            experiment = experiment / Input.Length;
            
        }

        // https://www.math.uh.edu/~jiwenhe/math4377/lectures/sec2_2.pdf
            public double linearmapping(byte Input) 
            {
       // https://stackoverflow.com/questions/57823085/my-linear-map-function-is-not-giving-right-answers
            var newSize = 1 - (-1);
            var oldSize = 255 - 0;
            var oldScale = (double)Input - 0;
            return (newSize * oldScale / oldSize) + (-1);
        
            }

        }

    
}
