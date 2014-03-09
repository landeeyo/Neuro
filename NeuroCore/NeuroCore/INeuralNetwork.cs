
using System;
namespace NeuroCore
{
    public interface INeuralNetwork
    {
        void AddNeuron(INeuron neuron);
        void AddConnection(IConnection connection);
        void RemoveNeuron(INeuron neuron);
        void RemoveConnection(IConnection connection);

        string PrintNeuronList();
        string PrintConnectionList();
        bool IsConnection(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation);
        double GetDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation);

        void Tick();
    }
}
