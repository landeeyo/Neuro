using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using log4net;

namespace NeuroCore
{
    public class SimpleNeuralNetwork : INeuralNetwork
    {
        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        List<INeuron> neurons;
        List<IConnection> connections;
        long tickCount;

        public SimpleNeuralNetwork()
        {
            logger.Debug("Initializing neural network [" + this.GetHashCode().ToString() + "]");
            neurons = new List<INeuron>();
            connections = new List<IConnection>();
            tickCount = 0;
        }

        public void AddNeuron(INeuron neuron)
        {
            logger.Debug("Adding neuron");
            neurons.Add(neuron);
        }

        public void AddConnection(IConnection connection)
        {
            logger.Debug("Adding connection");
            connections.Add(connection);
        }

        public void RemoveNeuron(INeuron neuron)
        {
            logger.Debug("Removing neuron");
            neurons.Remove(neuron);
        }

        public void RemoveConnection(IConnection connection)
        {
            logger.Debug("Removing connection");
            connections.Remove(connection);
        }

        public string PrintNeuronList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (INeuron neuron in neurons)
            {
                //sb.AppendLine(neuron.GetHashCode().ToString());
                sb.AppendLine("loc: [" + neuron.Location.Item1 + "," + neuron.Location.Item2 + "," + neuron.Location.Item3 + "]");
                sb.AppendLine("");
            }
            return sb.ToString();
        }

        public string PrintConnectionList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IConnection connection in connections)
            {
                //sb.AppendLine(connection.GetHashCode().ToString());
                sb.AppendLine(connection.ConnectionDescription());
                sb.AppendLine("mf: " + connection.GetMyelinationFactor.ToString());
                sb.AppendLine("dist: " + connection.GetDistance.ToString());
                sb.Append("");
            }
            return sb.ToString();
        }

        public bool IsConnection(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation)
        {
            foreach (IConnection connection in connections)
            {
                if (connection.SourceNeuron.Location == firstLocation && connection.DestinationNeuron.Location == secondLocation)
                    return true;
            }
            return false;
        }

        public double GetDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation)
        {
            if (IsConnection(firstLocation, secondLocation))
            {
                var connection = from conn in connections
                                 where conn.SourceNeuron.Location == firstLocation
                                 && conn.DestinationNeuron == secondLocation
                                 select conn;
                return connections.FirstOrDefault().GetDistance;

            }
            else
            {
                return -1.0;
            }
        }

        public void Tick()
        {
            logger.Debug("Ticking network");
            IList<INeuron> firedNeurons = new List<INeuron>();
            foreach (INeuron n in neurons)
            {
                n.Tick();
                if (n.IsOutput())
                {
                    firedNeurons.Add(n);
                }
            }
            foreach (IConnection c in connections)
            {
                if (firedNeurons.Contains(c.SourceNeuron))
                {
                    c.Fire();
                }
                c.Tick();
            }
            tickCount++;
            logger.Debug("Network has been ticked (tick no. " + tickCount + ")");
        }
    }
}
