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
            

            public void inputlayer(byte[] Input)
            {
              inputs = new double[Input.Length];
                for (int i = 0; i < Input.Length; i++)
                {
                    inputs[i] = ((double)Input[i])/255;
                }
            }

          

        }

    
}
