using System;
using log4net;

namespace NeuroCore
{
    public class SimpleNeuron : INeuron
    {
        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        bool _active;
        Tuple<int, int, int> _location;
        int _potential;
        int _activationThreshold;
        int _roundsToActivation;

        public int ActivationThreshold
        {
            get { return _activationThreshold; }
            set { _activationThreshold = value; }
        }

        public SimpleNeuron(Tuple<int, int, int> location)
        {
            _active = true;
            _potential = 0;
            _location = location;
            _activationThreshold = 25;
        }

        /// <summary>
        /// Shortly after fire is deactivated for some time
        /// </summary>
        public bool IsActive
        {
            get { return _active; }
        }

        /// <summary>
        /// Signal on input
        /// </summary>
        public void Input()
        {
            logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has new input.");
            _potential++;
        }

        public void TryActivate()
        {
            if (_roundsToActivation == 0)
            {
                _active = true;
            }
        }

        /// <summary>
        /// Deactivation
        /// </summary>
        public void Deactivate()
        {
            logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has been deactivated.");
            _potential = 0;
            _active = false;
            _roundsToActivation = 10;
        }

        public bool IsOutput()
        {
            if (_active)
            {
                if (_potential >= _activationThreshold)
                {
                    logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has fired.");
                    Deactivate();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Tuple<int, int, int> Location
        {
            get { return _location; }
        }

        public void Tick()
        {
            if (_roundsToActivation > 1)
            {
                _roundsToActivation -= 1;
            }
            TryActivate();
            //if (_active)
            //{
            //    IsOutput();
            //}
            logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has been ticked.");
        }
    }
}
