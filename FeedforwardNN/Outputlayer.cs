using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{

    // Here the weightlayer gets processed, activation and back prop are run here
    // here the number that has the most chance gets converted to a number 0-9 as output, also return output to back
    class outputlayer
    {

        public FeedforwardNN network;
        public Hiddenlayer hiddenlayer;
        public double MSE; // we want to give the MSE of this whole pattern


        public int nNeurons = 10;

        public outputlayer(Hiddenlayer Hiddenlayer, FeedforwardNN Network)
        {
        
            this.hiddenlayer = Hiddenlayer;
            this.network = Network;

        
        } 

        public void activate()
        {
            this.hiddenlayer.forwardprop();
        }
        
        public void error()
        {
            hiddenlayer.error();
            MSE = hiddenlayer.sumerror;
        }

    }


}
