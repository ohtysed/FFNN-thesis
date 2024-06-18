using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{
    // in this layer we give the neurons activation, backprop, error, learning(backprop etc if more) as property
    // to use in the outputlayer  
    class Hiddenlayer
    {
        // squared minimum error
        // https://www.deeplearningbook.org/contents/mlp.html page 200  for backprop
        public neuron[] neurons;
        public setinputlayer il; // this so we can acces the inputlayer stuff

        public double[] Sigmasum = new double[10]; // we make an array for every neuron to have their own sigmoid output

        public double sumerror = 0;

        public FeedforwardNN network;

        // We receive the input from an object which is the inputlayer and we first make them neurons to perform forwrd prop
        public Hiddenlayer(setinputlayer IL, FeedforwardNN Network)
        {

            network = Network;
            il = IL;

            // we make 10 neurons of the input
            int nneurons = 10;
            neurons = new neuron[nneurons];
            for (int i = 0; i < nneurons; i++)
            {
                neurons[i] = new neuron(this.il.inputs);
            }

        }
         
        public void setNeuron()
        {
            int nneurons = 10;
            for (int i = 0; i < nneurons; i++)
            {
                neurons[i].setneuron(this.il.inputs);
        }

        }
        
        public void forwardprop()
        {
            foreach (var neuron in this.neurons)
            {
                neuron.forwardprop();

            }
        }


        public void activate()
        {
            int i = 0;
            foreach (var neuron in this.neurons)
            {

                neuron.activation();

                Sigmasum[i] = neuron.sigmoidsum;
                i++;
                 
            }

        }


        public void backprop()
        {

        }

        public void error()
        {
            double max = Sigmasum[(int)il.expect]; // we say this is the max 
            
            int i = 0;
            foreach (var estimate in Sigmasum)
            {

                if (i != il.expect)
                {
                    if (estimate == 0)
                    {
                        sumerror += 0;
                    }
                    else
                    {
                        sumerror +=  0.5 * (Math.Pow( (-1 - estimate), 2 )); // desired state is -1 if we are not wanting the neuron to activate
                    }
                   
                }
                else
                {
                    if (estimate==0)
                    {
                        sumerror += 0;
                    }
                    else
                    {
                        sumerror += 0.5 * (Math.Pow(1 - estimate, 2)); // desired state is 1 if we want the neuron to activate
                    }
                   
                }
                i++;

                if (max < estimate && i == ((int)il.expect)) // small little true true output 
                {
                    network.wrong += 1;
                }
            
            }


        }



    }

    class neuron
    {

        // this is one neuron, it has 28*28 pixels of weight per neuron, the output is then
        // sum of dot product of wx, and so outputs 1 output, 1 out of 10
        // Every neuron has an intrinsic value which has the following properties 
        // - It calculates every sum of error based on: TODO
        // - it calculates also the activation value which is the sigmoid function -> calculates chance 

        public double error = 0;
        public double sum = 0;
        public double sigmoidsum = 0;
        public double expected = 0;


        public double[] input = new double[28 * 28];
        public double[] weights = new double[28*28];  // this is one neuron
        public double[] oldweights = new double[28*28];
        public neuron (double[] h)
        { 
            initializeweights();
            input = h;
        }

        public void setneuron(double[] h)
        {
            input = h;  

        }

        // Xavier initiliazation // todo: change initiliazation of weights https://arxiv.org/pdf/2004.06632
        public void initializeweights()
        {
            Random rand = new Random();
            double variance = Math.Sqrt(6/(11)); // Limit idk actually change when all works lol todo
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rand.NextDouble(); 
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

        // we forward prop which is giving calculating for every neuron summation of w_i*x
        public void forwardprop()
        {
            sum = 0;
            int i = 0;
            foreach (var item in input)
            {
                if (item == 0)
                {
                    sum += 0;
                }
                else
                {
                    sum += item * weights[i];
                }
              
                i++;
            }
            
        }

        // Hello, this is the activation with is the sigmoid function my friend
        // https://en.wikipedia.org/wiki/Sigmoid_function
        public void activation()
        {
            if (sum == 0)
            {
                sigmoidsum = 0;
            }
            else
            {
                sigmoidsum = (1) / (1 + Math.Exp(-sum));
            }
            
        }

     
        public void backpropagate()
        {


        }



    }




}
