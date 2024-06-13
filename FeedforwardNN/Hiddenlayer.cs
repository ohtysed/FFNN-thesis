using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    // hidden layer maybe multiple in future, here the neurons will receive activation, learning(backprop etc if more) 
    class Hiddenlayer
    {
        // squared minimum error
        // https://www.deeplearningbook.org/contents/mlp.html page 200  for backprop
        public neuron[] neurons;
        public inputlayer il; // this so we can acces the inputlayer stuff

        // We receive the input from the previous layer and perform function z/forward prop
        public Hiddenlayer(inputlayer IL)
        {
            il = IL;
            int nneurons = 10;
            neurons = new neuron[nneurons];
            for (int i = 0; i <= nneurons; i++)
            {
                neurons[i] = new neuron(this);
            }


        }

      
        
        public void backprop()
        {

        }




    }

    class neuron
    {
        // Here we define a single neuron
        // Every neuron has an intrinsic value which has the following properties 
        // - It calculates every sum of error based on: TODO
        // - it calculates also the activation value which is the sigmoid function -> calculates chance 

        public double error = 0;
        public double sum = 0;
        public double sigmsum = 0;
        public double expected = 0;

        public Hiddenlayer hiddenlayer;

        public double[] weights = new double[28*28];
        public double[] oldweights = new double[28*28];
        public neuron (Hiddenlayer h)
        { 
            initializeweights(); 
            hiddenlayer = h;
        }


        // Xavier initiliazation
        public void initializeweights()
        {
            Random rand = new Random();
            double variance = Math.Sqrt(6/(28*28+10)); // Limit
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rand.NextDouble() * 2 * variance - variance; 
            }
        }


        // copied from https://stackoverflow.com/questions/218060/random-gaussian-variables
        // https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        private static double boxMuller(double mean, double std)
        {
            Random r = new Random();
            // pick 2 random numbers 
            double x1 = 1.0 - r.NextDouble();  
            double x2 = 1.0 - r.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(x1)) *
            Math.Sin(2.0 * Math.PI * x2); 
            return  mean + std * randStdNormal; 

        }

        // we forward prop and activate 
        public void forwardprop()
        {
            sum = 0;
            int i = 0;
            foreach (var item in hiddenlayer.il.inputs)
            {
                sum += item *weights[i];
                i++;
            }
        

            activation();
        }


        //it is the activation function ReLU function
        public void activation()
        {
            sigmsum = Math.Max(sum, 0);
        }

        //CAN IMPLEMENT PARTLY ERROR FUNCTION/CONSTANT HERE 
        public void Error() // error function is wrong i think, should be mean squared error?
        {
            if (sigmsum > 0)
            {
                error = 1 * (expected * sigmsum); // derivative of ReLU is 1
            }

            error = 0; // else there is no error 
           
        }
     
        public void backpropagate()
        {


        }



    }




}
