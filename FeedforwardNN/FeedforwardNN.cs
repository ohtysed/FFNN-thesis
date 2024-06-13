using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    class FeedforwardNN
    {
            // These are the main layers, the output and hidden layers are connected by giving the parameter 'this' which references the class feedforwardNN
        public inputlayer inputlayer;
        public outputlayer outputlayer; 
        public Hiddenlayer hiddenlayer;

        public double learningrate = 0.01;
        public double MSE = 0; // for all patterns tot average

        public double erMSE;

        public byte[] newinpuT;

        public int expect;


        public FeedforwardNN() { create(newinpuT); }


        public void setexpectation(int expected)
        {

            expect = expected;

        }

        // maybe give here also inputlayer the image so it can read all(?)
        public void create(byte[] newinput)
        {
            inputlayer = new inputlayer(newinput, expect);   //  todo give image
            hiddenlayer = new Hiddenlayer(inputlayer,this); // we give inputlayer as object to the weight layer and also
                                                            // the network so it can calculate the error
            outputlayer = new outputlayer(hiddenlayer, this); // then we give the output the weight layer as object and this FFNN
                                                        
        }


        public void active()
        {
            // try to do all here, so active is training on given image



        }
      
    }
}
