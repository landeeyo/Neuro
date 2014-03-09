using System;
using System.Collections.Generic;
using NeuroCore;
using log4net;
using log4net.Config;

namespace BasicClient
{
    class Program
    {
        //private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            //XmlConfigurator.Configure(new System.IO.FileInfo("log.config"));

            //logger.Debug("test");

            INeuralNetwork nn = new SimpleNeuralNetwork();

            #region Adding neurons

            SimpleNeuron n1 = new SimpleNeuron(new Tuple<int, int, int>(1, 1, 1));
            SimpleNeuron n2 = new SimpleNeuron(new Tuple<int, int, int>(10, 3, 2));
            SimpleNeuron n3 = new SimpleNeuron(new Tuple<int, int, int>(4, 6, 5));
            SimpleNeuron n4 = new SimpleNeuron(new Tuple<int, int, int>(10, 1, 3));
            SimpleNeuron n5 = new SimpleNeuron(new Tuple<int, int, int>(6, 6, 6));
            SimpleNeuron n6 = new SimpleNeuron(new Tuple<int, int, int>(1, 4, 2));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);
            nn.AddNeuron(n3);
            nn.AddNeuron(n4);
            nn.AddNeuron(n5);
            nn.AddNeuron(n6);


            #endregion

            #region Connecting neurons

            nn.AddConnection(new SimpleConnection(n1, n3));
            nn.AddConnection(new SimpleConnection(n3, n1));
            nn.AddConnection(new SimpleConnection(n6, n2));
            nn.AddConnection(new SimpleConnection(n1, n4));
            nn.AddConnection(new SimpleConnection(n3, n4));
            nn.AddConnection(new SimpleConnection(n5, n6));
            nn.AddConnection(new SimpleConnection(n3, n2));
            nn.AddConnection(new SimpleConnection(n5, n1));

            #endregion

            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            int n = 100000;

            for (int i = 0; i < n; i++)
            {
                //if (i % 123 == 0)
               // {
                    n1.Input();
               // }
                //if (i % 124 == 0)
                //{
                    n6.Input();
                //}
                //if (i % 125 == 0)
                //{
                    n3.Input();
                //}
                nn.Tick();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            for (int i = 0; i < n; i++)
            {
                nn.Tick();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            Console.ReadLine();
        }
    }
}
