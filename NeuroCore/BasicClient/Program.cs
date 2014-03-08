using System;
using System.Collections.Generic;
using NeuroCore;

namespace BasicClient
{
    class Program
    {
        static void Main(string[] args)
        {
            INeuralNetwork nn = new SimpleNeuralNetwork();

            #region Adding neurons

            SimpleNeuron n1 = new SimpleNeuron(new Tuple<int, int, int>(1, 1, 1));
            SimpleNeuron n2 = new SimpleNeuron(new Tuple<int, int, int>(10, 3, 2));
            SimpleNeuron n3 = new SimpleNeuron(new Tuple<int, int, int>(4, 6, 5));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);
            nn.AddNeuron(n3);

            #endregion

            #region Connecting neurons

            nn.AddConnection(new SimpleConnection(n1, n3));
            nn.AddConnection(new SimpleConnection(n3, n1));

            #endregion

            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            Console.ReadLine();
        }
    }
}
