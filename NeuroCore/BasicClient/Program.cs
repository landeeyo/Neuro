using System;
using System.Collections.Generic;
using NeuroCore;
using log4net;
using log4net.Config;

namespace BasicClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Test2();
        }

        private static void Test1()
        {
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

        private static void Test2()
        {
            INeuralNetwork nn = new SimpleNeuralNetwork();
            nn.NeuronFireTreshold = 20;
            nn.NeuronReactivationTimeFactor = 5;
            nn.ConnectionMyelinationGrowthFactor = 20;
            nn.ConnectionMyelinationDeclineFactor = 1;

            #region Adding neurons

            SimpleNeuron n1 = new SimpleNeuron(new Tuple<int, int, int>(1, 1, 1));
            SimpleNeuron n2 = new SimpleNeuron(new Tuple<int, int, int>(10, 3, 2));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);

            #endregion

            #region Connecting neurons

            nn.AddConnection(new SimpleConnection(n1, n2));

            #endregion

            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            int firstLoop = 100;
            int secondLoop = 250;

            Console.WriteLine("First loop (input): ");

            for (int i = 0; i < firstLoop; i++)
            {
                n1.Input();
                nn.Tick();
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(nn.PrintNeuronList());
                    Console.Write(nn.PrintConnectionList());
                    Console.ReadKey();
                }
            }
            
            Console.WriteLine("Second loop (no input): ");

            for (int i = 0; i < secondLoop; i++)
            {
                nn.Tick();
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(nn.PrintNeuronList());
                    Console.Write(nn.PrintConnectionList());
                    Console.ReadKey();
                }
            }

            Console.ReadLine();
        }

        private static void Test3()
        {
            INeuralNetwork nn = new SimpleNeuralNetwork();
            nn.NeuronFireTreshold = 20;
            nn.NeuronReactivationTimeFactor = 5;
            nn.ConnectionMyelinationGrowthFactor = 10;
            nn.ConnectionMyelinationDeclineFactor = 1;

            #region Adding neurons

            SimpleNeuron n1 = new SimpleNeuron(new Tuple<int, int, int>(1, 1, 1));
            SimpleNeuron n2 = new SimpleNeuron(new Tuple<int, int, int>(10, 3, 2));
            SimpleNeuron n3 = new SimpleNeuron(new Tuple<int, int, int>(5, 6, 1));
            SimpleNeuron n4 = new SimpleNeuron(new Tuple<int, int, int>(6, 6, 6));
            SimpleNeuron n5 = new SimpleNeuron(new Tuple<int, int, int>(4, 7, 9));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);
            nn.AddNeuron(n3);
            nn.AddNeuron(n4);
            nn.AddNeuron(n5);

            #endregion

            #region Connecting neurons

            nn.AddConnection(new SimpleConnection(n1, n3));
            nn.AddConnection(new SimpleConnection(n1, n4));
            nn.AddConnection(new SimpleConnection(n2, n3));
            nn.AddConnection(new SimpleConnection(n2, n4));
            nn.AddConnection(new SimpleConnection(n3, n5));
            nn.AddConnection(new SimpleConnection(n4, n5));

            #endregion

            Console.WriteLine(nn.PrintNeuronList());
            Console.Write(nn.PrintConnectionList());

            int n = 250;

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    n1.Input();
                }
                else
                {
                    n2.Input();
                }
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
