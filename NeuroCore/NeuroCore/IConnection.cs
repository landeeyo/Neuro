using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroCore
{
    public interface IConnection
    {
        int GetMyelinationFactor { get; }
        bool IsActive { get; }
        int GetRemainingRounds { get; }
        IList<INeuron> GetInputNeuronList { get; }
        IList<INeuron> GetOutputNeuronList { get; }
        
        void AddInputNeuron(INeuron neuron);
        void DeleteInputNeuron(INeuron neuron);
        void AddOutputNeuron(INeuron neuron);
        void DeleteOutputNeuron(INeuron neuron);
    }
}
