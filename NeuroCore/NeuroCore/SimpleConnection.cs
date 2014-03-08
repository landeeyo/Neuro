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
        INeuron _sourceNeuron;
        INeuron _destinationNeuron;

        public SimpleConnection()
        {
            _myelinationFactor = 1;
            _active = false;
            _remainingRounds = 0;
        }

        public SimpleConnection(INeuron sourceNeuron, INeuron destinationNeuron)
        {
            _myelinationFactor = 1;
            _active = false;
            _remainingRounds = 0;
            _sourceNeuron = sourceNeuron;
            _destinationNeuron = destinationNeuron;
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

        private double CalculateDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondlocation)
        {
            double deltaX = secondlocation.Item1 - firstLocation.Item1;
            double deltaY = secondlocation.Item2 - firstLocation.Item2;
            double deltaZ = secondlocation.Item3 - firstLocation.Item3;
            return (double)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        public double GetDistance
        {
            get { return CalculateDistance(SourceNeuron.Location, DestinationNeuron.Location); }
        }

        public INeuron SourceNeuron
        {
            get { return _sourceNeuron; }
            set { _sourceNeuron = value; }
        }

        public INeuron DestinationNeuron
        {
            get { return _destinationNeuron; }
            set { _destinationNeuron = value; }
        }
    }
}
