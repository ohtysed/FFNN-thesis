using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedforwardNN
{

    
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

        public void forwardprop() { this.hiddenlayer.forwardprop(); }

        public void activate()    { this.hiddenlayer.activate();  }

        public void backprop() { this.hiddenlayer.backpropagate(); }
        
        public void error()
        {
            hiddenlayer.error();
            MSE = hiddenlayer.sumerror;
        }

    }


}
