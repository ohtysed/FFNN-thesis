using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public double learningrate = 0.01;
        public double MSEtrain = 0;
         
        public int wrong = 0;
        public double expect = 0.0;

        public Collection<Image> training_data;
        public Collection<Image> test_data;

  



        // we initialize all the things of the NN
        public FeedforwardNN(Collection<Image> Train_data, Collection<Image> Test_data) 
        {

            training_data = Train_data;
            test_data = Test_data;
            create();

            
        }

        // we read and train all the training data
        public void readandtrainpattern() 
        {
            foreach (var item in training_data)
            {
                inputlayer.inputlayer(item.Data);
                hiddenlayer.setNeuron();
                setexpectation(item.Label);
                active();
                MSEtrain += outputlayer.MSE;

            }

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



        public double MSEerror() 
        {
            return MSEtrain / training_data.Count(); //  to conclude the final error we divide by total patterns, should only be done at the end
        }

        public void active()
        {
            outputlayer.forwardprop();
            outputlayer.activate();
            outputlayer.error();
        }
      
    }
}
