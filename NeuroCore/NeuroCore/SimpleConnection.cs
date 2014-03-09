using System;
using System.Collections.Generic;
using System.Collections;
using log4net;

namespace NeuroCore
{
    public class SimpleConnection : IConnection
    {
        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

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

        /// <summary>
        /// When neuron fires signal
        /// </summary>
        public void Fire()
        {
            if (_active == false)
            {
                _active = true;
                _remainingRounds = Convert.ToInt32(10.0 * GetDistance / _myelinationFactor);
                logger.Debug( "Connection " + ConnectionDescription() + " has been fired. Remaining rounds: " + _remainingRounds);
            }
        }

        /// <summary>
        /// When network round is executed
        /// </summary>
        public void Tick()
        {
            if (_active)
            {
                if (_myelinationFactor < 10000)
                {
                    _myelinationFactor += 100;
                }
                if (_remainingRounds == 0)
                {
                    DestinationNeuron.Input();
                    _active = false;
                }
                if (_remainingRounds > 0)
                {
                    _remainingRounds -= 1;
                }
            }
            else
            {
                //if (_myelinationFactor > 51)
                //{
                //    _myelinationFactor -= 50;
                //}
                //else if (_myelinationFactor > 1 && _myelinationFactor<51)
                //{
                //    _myelinationFactor = 1;
                //}
                if (_myelinationFactor > 2)
                {
                    _myelinationFactor -= 1;
                }
            }
            logger.Debug("Connection " + ConnectionDescription() + " has been ticked");
            logger.Debug("Activity status: " + IsActive.ToString());
            logger.Debug("Remaining rounds: " + _remainingRounds.ToString());
            logger.Debug("MyelinationFactor: " + _myelinationFactor.ToString());
        }

        #region Log methods

        public string ConnectionDescription()
        {
            return string.Format("[ {0}, {1}, {2} ] ---> [ {3}, {4}, {5} ]", SourceNeuron.Location.Item1, SourceNeuron.Location.Item2, SourceNeuron.Location.Item3, DestinationNeuron.Location.Item1, DestinationNeuron.Location.Item2, DestinationNeuron.Location.Item3);
        }

        #endregion
    }
}
