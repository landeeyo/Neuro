using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace NeuroCore
{
    public class SimpleNeuron : INeuron
    {
        bool _active;
        Tuple<int, int> _location;
        int _potential;
        int _activationThreshold;

        public int ActivationThreshold
        {
            get { return _activationThreshold; }
            set { _activationThreshold = value; }
        }

        SimpleNeuron(Tuple<int,int> location)
        {
            _active = true;
            _potential = 0;
            _location = location;
        }

        public bool IsActive
        {
            get { return _active; }
        }

        public Tuple<int, int> GetLocation
        {
            get { return _location; }
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
    }
}
