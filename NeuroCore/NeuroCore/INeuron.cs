using System;

namespace NeuroCore
{
    public interface INeuron
    {
        bool IsActive { get; }
        Tuple<int, int, int> Location { get; }
        int FireTreshold { get; set; }
        int ReactivationTimeFactor { get; set; }
        
        void Input();
        void Deactivate();
        bool IsOutput();
        void Tick();
    }
}
