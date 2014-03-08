using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroCore
{
    public interface INeuralNetwork
    {
        void AddNeuron(INeuron neuron);
        void AddConnection(IConnection connection);
        void DeleteNeuron(INeuron neuron);
        void DeleteConnection(IConnection connection);

        string PrintNeuronList();
        string PrintConnectionList();
    }
}
