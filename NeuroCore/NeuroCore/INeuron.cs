using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NeuroCore
{
    public interface INeuron
    {
        bool IsActive { get; }
        Tuple<int, int> GetLocation { get; }
        int ActivationThreshold { get; set; }

        void Input();
        void Deactivate();
        bool IsOutput();
    }
}
