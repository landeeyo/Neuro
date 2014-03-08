using System;

namespace NeuroCore
{
    public interface INeuron
    {
        bool IsActive { get; }
        Tuple<int, int, int> Location { get; }
        int ActivationThreshold { get; set; }

        void Input();
        void Deactivate();
        bool IsOutput();
    }
}
