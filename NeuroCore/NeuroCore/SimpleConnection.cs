using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuroCore
{
    public class SimpleConnection : IConnection
    {
        int _myelinationFactor;
        bool _active;
        int _remainingRounds;
        List<INeuron> _inputNeuronList;
        List<INeuron> _outputNeuronList;

        public int GetMyelinationFactor
        {
            get { return _myelinationFactor; }
        }

        public bool IsActive
        {
            get { return _active; }
        }

        public int GetRemainingRounds
        {
            get { return _remainingRounds; }
        }

        public IList<INeuron> GetInputNeuronList
        {
            get { return _inputNeuronList; }
        }

        public IList<INeuron> GetOutputNeuronList
        {
            get { return _outputNeuronList; }
        }

        public void AddInputNeuron(INeuron neuron)
        {
            throw new NotImplementedException();
        }

        public void DeleteInputNeuron(INeuron neuron)
        {
            throw new NotImplementedException();
        }

        public void AddOutputNeuron(INeuron neuron)
        {
            throw new NotImplementedException();
        }

        public void DeleteOutputNeuron(INeuron neuron)
        {
            throw new NotImplementedException();
        }
    }
}
