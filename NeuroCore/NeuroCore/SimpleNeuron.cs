using System;
using log4net;

namespace NeuroCore
{
    public class SimpleNeuron : INeuron
    {
        #region Data fields

        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        bool _active;
        Tuple<int, int, int> _location;
        int _potential;
        int _fireTreshold;
        int _reactivationTimeFactor;
        int _roundsToActivation;

        #endregion

        #region Properties

        public int ReactivationTimeFactor
        {
            get { return _reactivationTimeFactor; }
            set { _reactivationTimeFactor = value; }
        }

        public int FireTreshold
        {
            get { return _fireTreshold; }
            set { _fireTreshold = value; }
        }

        public int RoundsToActivation
        {
            get { return _roundsToActivation; }
            set { _roundsToActivation = value; }
        }

        public Tuple<int, int, int> Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Shortly after fire is deactivated for some time
        /// </summary>
        public bool IsActive
        {
            get { return _active; }
        }

        #endregion

        #region Init

        public SimpleNeuron(Tuple<int, int, int> location)
        {
            _active = true;
            _potential = 0;
            _location = location;
        }

        #endregion

        #region Network related methods

        /// <summary>
        /// Signal on input
        /// </summary>
        public void Input()
        {
            logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has new input.");
            logger.Debug("potential: " + _potential);
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
            _roundsToActivation = _reactivationTimeFactor;
        }

        public bool IsOutput()
        {
            if (_active)
            {
                if (_potential >= _fireTreshold)
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

        public void Tick()
        {
            if (_roundsToActivation > 1)
            {
                _roundsToActivation -= 1;
            }
            TryActivate();
            logger.Debug("Neuron [" + Location.Item1 + "," + Location.Item2 + "," + Location.Item3 + "] has been ticked.");
        }

        #endregion
    }
}
