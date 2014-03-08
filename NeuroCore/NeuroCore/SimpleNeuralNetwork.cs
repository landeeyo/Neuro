using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NeuroCore
{
    [Serializable]
    public class SimpleNeuralNetwork : INeuralNetwork
    {
        List<INeuron> neurons;
        List<IConnection> connections;

        public SimpleNeuralNetwork()
        {
            neurons = new List<INeuron>();
            connections = new List<IConnection>();
        }

        public void AddNeuron(INeuron neuron)
        {
            neurons.Add(neuron);
        }

        public void AddConnection(IConnection connection)
        {
            connections.Add(connection);
        }

        public void DeleteNeuron(INeuron neuron)
        {
            neurons.Remove(neuron);
        }

        public void DeleteConnection(IConnection connection)
        {
            connections.Remove(connection);
        }

        public string PrintNeuronList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (INeuron neuron in neurons)
            {
                sb.AppendLine(neuron.GetHashCode().ToString());
            }
            return sb.ToString();
        }

        public string PrintConnectionList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IConnection connection in connections)
            {
                sb.AppendLine(connection.GetHashCode().ToString());
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
    }
}
