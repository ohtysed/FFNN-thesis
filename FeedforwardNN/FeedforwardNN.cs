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
        public static inputlayer inputlayer;
        public static outputlayer outputlayer; 
        public static Hiddenlayer hiddenlayer;

        public double learningrate = 0.01;

        public static int expect;

        public void setexpectation(int expected)
        {

            expect = expected;

        }

        public void create()
        {
            inputlayer = new inputlayer();
            outputlayer = new outputlayer();    
            hiddenlayer = new Hiddenlayer(inputlayer); // we give this neural network as object to the weight layer as wel as the inputlayer
        }
        public FeedforwardNN() { create(); }
    }
}
