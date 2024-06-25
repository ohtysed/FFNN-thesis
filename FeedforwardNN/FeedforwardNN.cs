using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
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

        public double learningrate = 0.1;
        public double MSEtrain = 0;
        public double testMSEtrain = 0;
        public double epochs = 1000;

 
         
        public int wrong = 0;
        public int testwrong = 0;
    
        public int expect = 0;
        public double Experiment; // calculates average of pixels in a set

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
            for (int i = 0;  i < epochs; i++) // the training set was repeated 30 times and not the patterns themselves
            {
                wrong = 0; // we reset wrong counter after every epoch
                foreach (var item in training_data) 
                {

                    inputlayer.inputlayer(item.Data);
                    outputlayer.setNeuron();
                    setexpectation(item.Label);
                    active();
                    MSEtrain += outputlayer.MSE;
                    outputlayer.backprop();
                    Experiment += inputlayer.experiment; 

                } 
                Experiment /= training_data.Count();
                Console.WriteLine((wrong) + " wrong on epoch" + i + " of training set");
                Console.WriteLine(((MSEtrain/(i+1))/(training_data.Count)) + " this is average MSE of training set");
                readandtestpattern(); // now we also want to do the test training set to see how well the weight are doing
            

                // Console.WriteLine(Experiment+ " this is average pixels");
                //Console.WriteLine(alsowrong + " if output was 1 how many others are also 1");
            }
        

        }

        public void readandtestpattern()
        {
            testwrong = 0;  // reset all
            foreach (var item in test_data) 
            {
                inputlayer.inputlayer(item.Data);
                outputlayer.setNeuron();
                setexpectation(item.Label);
            //    Console.WriteLine(  item.Label + " actual label");
              //  Console.WriteLine(expect+ " network label");
                activetest();
               

            } 
            Console.WriteLine(testwrong + " amount wrong in test");

        }


        public void setexpectation(int expected)
        {

            expect = expected;

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
            return MSEtrain / training_data.Count() / epochs; //  to conclude the final error we divide by total patterns, should only be done at the end
        }

        public void activetest()
        {
            outputlayer.testforwardprop();
            outputlayer.testactivate();
            outputlayer.testerror();

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
