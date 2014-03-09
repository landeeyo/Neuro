using System;
using System.Collections.Generic;
using System.Collections;
using log4net;

namespace NeuroCore
{
    public class SimpleConnection : IConnection
    {
        #region Data fields

        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        int _myelinationFactor;
        bool _active;
        int _remainingRounds;
        INeuron _sourceNeuron;
        INeuron _destinationNeuron;
        int _myelinationGrowthFactor;
        int _myelinationDeclineFactor;
        readonly int _myelinationFactorPeak = 10000;

        #endregion

        #region Properties

        public int GetMyelinationFactor
        {
            get { return _myelinationFactor; }
        }

        public int MyelinationGrowthFactor
        {
            get { return _myelinationGrowthFactor; }
            set { _myelinationGrowthFactor = value; }
        }

        public bool IsActive
        {
            get { return _active; }
        }

        public int GetRemainingRounds
        {
            get { return _remainingRounds; }
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

        public int MyelinationDeclineFactor
        {
            get { return _myelinationDeclineFactor; }
            set { _myelinationDeclineFactor = value; }
        }

        public double GetDistance
        {
            get { return CalculateDistance(SourceNeuron.Location, DestinationNeuron.Location); }
        }

        public int GetRoundDistance
        {
            get { return Convert.ToInt32((_myelinationFactorPeak / 10.0 * GetDistance) / _myelinationFactor); ; }
        }

        #endregion

        #region Init

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

        #endregion

        #region Network related methods

        /// <summary>
        /// When neuron fires signal
        /// </summary>
        public void Fire()
        {
            if (_active == false)
            {
                _active = true;
                _remainingRounds = Convert.ToInt32((_myelinationFactorPeak / 10.0 * GetDistance) / _myelinationFactor);
                logger.Debug("Connection " + ConnectionDescription() + " has been fired. Remaining rounds: " + _remainingRounds);
            }
        }

        /// <summary>
        /// When network round is executed
        /// </summary>
        public void Tick()
        {
            if (_active)
            {
                if (_myelinationFactor + _myelinationGrowthFactor < _myelinationFactorPeak)
                {
                    _myelinationFactor += _myelinationGrowthFactor;
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
                if (_myelinationFactor - _myelinationDeclineFactor >= 1)
                {
                    _myelinationFactor -= _myelinationDeclineFactor;
                }
            }
            logger.Debug("Connection " + ConnectionDescription() + " has been ticked");
            logger.Debug("Activity status: " + IsActive.ToString());
            logger.Debug("Remaining rounds: " + _remainingRounds.ToString());
            logger.Debug("MyelinationFactor: " + _myelinationFactor.ToString());
        }

        private double CalculateDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondlocation)
        {
            double deltaX = secondlocation.Item1 - firstLocation.Item1;
            double deltaY = secondlocation.Item2 - firstLocation.Item2;
            double deltaZ = secondlocation.Item3 - firstLocation.Item3;
            return (double)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        #endregion

        #region Log methods

        public string ConnectionDescription()
        {
            return string.Format("[ {0}, {1}, {2} ] ---> [ {3}, {4}, {5} ]", SourceNeuron.Location.Item1, SourceNeuron.Location.Item2, SourceNeuron.Location.Item3, DestinationNeuron.Location.Item1, DestinationNeuron.Location.Item2, DestinationNeuron.Location.Item3);
        }

        #endregion
    }
}
