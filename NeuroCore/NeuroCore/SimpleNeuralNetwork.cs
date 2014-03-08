using System;
using System.Collections.Generic;
using System.Text;

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
            foreach(INeuron neuron in neurons)
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
    }
}
