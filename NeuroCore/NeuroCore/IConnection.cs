using System.Collections.Generic;
using System;

namespace NeuroCore
{
    public interface IConnection
    {
        int GetMyelinationFactor { get; }
        bool IsActive { get; }
        int GetRemainingRounds { get; }
        double GetDistance { get; }
        int GetRoundDistance { get; }
        INeuron SourceNeuron { get; set; }
        INeuron DestinationNeuron { get; set; }
        int MyelinationGrowthFactor { get; set; }
        int MyelinationDeclineFactor { get; set; }

        void Fire();
        void Tick();

        string ConnectionDescription();
    }
}

