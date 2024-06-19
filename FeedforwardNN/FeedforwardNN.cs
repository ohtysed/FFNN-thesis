﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FeedforwardNN
{
    class FeedforwardNN
    {
            // These are the main layers, the output and hidden layers are connected by giving the parameter 'this' which references the class feedforwardNN
        public setinputlayer inputlayer;
        public outputlayer outputlayer; 
        public Hiddenlayer hiddenlayer;

        public double learningrate = 0.4;
        public double MSEtrain = 0;
         
        public int wrong = 0;
        public double expect = 0.0;
        public double Experiment;

        public Collection<Image> training_data;
        public Collection<Image> test_data;

  



        // we initialize all the things of the NN
        public FeedforwardNN(Collection<Image> Train_data, Collection<Image> Test_data) 
        {

            training_data = Train_data;
            test_data = Test_data;
            create();

            
        }

        // we read and train all the training data, you can notice that I dont reset the weights anywhere,
        // only forward prop  &activate with new data
        //
        public void readandtrainpattern() 
        {
            for (int i = 0;  i < 30; i++)
            {
                foreach (var item in training_data) // the training set was repeated 30 times and not the patterns themselves
                {

                    inputlayer.inputlayer(item.Data);
                    hiddenlayer.setNeuron();
                    setexpectation(item.Label);
                    active();
                    MSEtrain += outputlayer.MSE;
                    outputlayer.backprop();
                    Experiment += inputlayer.experiment; 

                } // maybe implement here also error function over all patterns
                Experiment /= training_data.Count();
                Console.WriteLine(wrong + " amount wrong in training in epoch " + i);
               // Console.WriteLine(Experiment+ " this is average pixels");
            }
        

        }

        public void readandtestpattern()
        {
            wrong = 0;  // reset all
            MSEtrain = 0; // errors 
            foreach (var item in test_data) 
            {
              

                inputlayer.inputlayer(item.Data);
                hiddenlayer.setNeuron();
                setexpectation(item.Label);
                active();
                MSEtrain += outputlayer.MSE;
                //outputlayer.backprop();

            } 
            Console.WriteLine(wrong + " amount wrong in test");

        }


        public void setexpectation(double expected)
        {

            inputlayer.expect = expected;

        }

        // We have to create all the layers only 1 time, then we have to train all patterns on a single neural network
        public void create()
        {
            inputlayer = new setinputlayer();   
            hiddenlayer = new Hiddenlayer(inputlayer,this); // we give inputlayer as object to the weight layer and also
                                                            // the network so it can calculate the error
            outputlayer = new outputlayer(hiddenlayer, this); // likewise we give the output the weight layer as object and this FFNN
                                                        
        }


        // MSEerror represents the error for the average of all patterns
        public double MSEerror() 
        {
            return MSEtrain / training_data.Count(); //  to conclude the final error we divide by total patterns, should only be done at the end
        }




        /// <summary>
        /// this function does all, from forward prop, to activate and calculate error. It is always in this 
        /// order.
        /// </summary>
        public void active() 
        {
            outputlayer.forwardprop(); 
            outputlayer.activate();
            outputlayer.error();
           
        }
      
    }
}
