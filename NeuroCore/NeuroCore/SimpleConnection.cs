using System;
using System.Collections.Generic;
using System.Collections;

namespace NeuroCore
{
    public class SimpleConnection : IConnection
    {
        int _myelinationFactor;
        bool _active;
        int _remainingRounds;
        List<INeuron> _inputNeuronList;
        List<INeuron> _outputNeuronList;

        public SimpleConnection(List<INeuron> inputNeuronList, List<INeuron> outputNeuronList)
        {
            _myelinationFactor = 1;
            _active = false;
            _remainingRounds = 0;
            _inputNeuronList = new List<INeuron>();
            _inputNeuronList.AddRange(inputNeuronList);
            _outputNeuronList = new List<INeuron>();
            _outputNeuronList.AddRange(outputNeuronList);
        }

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

        public IList<Tuple<INeuron, INeuron, double>> GetDistances
        {
            get
            {
                IList<Tuple<INeuron, INeuron, double>> list = new List<Tuple<INeuron, INeuron, double>>();

                foreach (INeuron ineuron in _inputNeuronList)
                {
                    foreach (INeuron oneuron in _outputNeuronList)
                    {
                        double distance = this.CalculateDistance(ineuron.GetLocation, oneuron.GetLocation);
                        list.Add(new Tuple<INeuron, INeuron, double>(ineuron, oneuron, distance));
                    }
                }

                return list;
            }
        }

        private double CalculateDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondlocation)
        {
            double deltaX = secondlocation.Item1 - firstLocation.Item1;
            double deltaY = secondlocation.Item2 - firstLocation.Item2;
            double deltaZ = secondlocation.Item3 - firstLocation.Item3;
            return (double)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
}
