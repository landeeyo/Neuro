using System;

namespace NeuroCore
{
    public class SimpleNeuron : INeuron
    {
        bool _active;
        Tuple<int, int, int> _location;
        int _potential;
        int _activationThreshold;

        public int ActivationThreshold
        {
            get { return _activationThreshold; }
            set { _activationThreshold = value; }
        }

        public SimpleNeuron(Tuple<int,int, int> location)
        {
            _active = true;
            _potential = 0;
            _location = location;
        }

        public bool IsActive
        {
            get { return _active; }
        }

        public void Input()
        {
            _potential++;
        }

        public void Deactivate()
        {
            _active = false;
        }

        public bool IsOutput()
        {
            if (_potential >= _activationThreshold)
            {
                _potential = 0;
                _active = false;
                return true;
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
    }
}
