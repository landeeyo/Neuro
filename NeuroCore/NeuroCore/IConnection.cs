using System.Collections.Generic;
using System;

namespace NeuroCore
{
    public interface IConnection
    {
        int GetMyelinationFactor { get; }
        bool IsActive { get; }
        int GetRemainingRounds { get; }
        IList<INeuron> GetInputNeuronList { get; }
        IList<INeuron> GetOutputNeuronList { get; }
        IList<Tuple<INeuron,INeuron, double>> GetDistances { get; }
        
        void AddInputNeuron(INeuron neuron);
        void DeleteInputNeuron(INeuron neuron);
        void AddOutputNeuron(INeuron neuron);
        void DeleteOutputNeuron(INeuron neuron);
    }
}
