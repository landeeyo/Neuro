using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using log4net;

namespace NeuroCore
{
    public class SimpleNeuralNetwork : INeuralNetwork
    {
        #region Data fields

        #region Log4Net

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        List<INeuron> neurons;
        List<IConnection> connections;
        long tickCount;
        int _neuronFireTreshold;
        int _neuronReactivationTimeFactor;
        int _connectionMyelinationGrowthFactor;
        int _connectionMyelinationDeclineFactor;

        #endregion

        #region Init

        public SimpleNeuralNetwork()
        {
            logger.Debug("Initializing neural network [" + this.GetHashCode().ToString() + "]");
            neurons = new List<INeuron>();
            connections = new List<IConnection>();
            tickCount = 0;
            #region Default settings

            _neuronFireTreshold = 25;
            _neuronReactivationTimeFactor = 5;
            _connectionMyelinationGrowthFactor = 10;
            _connectionMyelinationDeclineFactor = 1;

            #endregion
        }

        #endregion

        #region Add and remove

        public void AddNeuron(INeuron neuron)
        {
            logger.Debug("Adding neuron");
            neurons.Add(neuron);
            RefreshNeuronsSettings();
        }

        public void AddConnection(IConnection connection)
        {
            logger.Debug("Adding connection");
            connections.Add(connection);
            RefreshConnectionsSettings();
        }

        public void RemoveNeuron(INeuron neuron)
        {
            logger.Debug("Removing neuron");
            neurons.Remove(neuron);
        }

        public void RemoveConnection(IConnection connection)
        {
            logger.Debug("Removing connection");
            connections.Remove(connection);
        }

        private void RefreshNeuronsSettings()
        {
            foreach (INeuron n in neurons)
            {
                n.FireTreshold = _neuronFireTreshold;
                n.ReactivationTimeFactor = _neuronReactivationTimeFactor;
            }
        }

        private void RefreshConnectionsSettings()
        {
            foreach (IConnection c in connections)
            {
                c.MyelinationGrowthFactor = _connectionMyelinationGrowthFactor;
                c.MyelinationDeclineFactor = _connectionMyelinationDeclineFactor;
            }
        }

        #endregion

        #region Printing

        public string PrintNeuronList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (INeuron neuron in neurons)
            {
                sb.AppendLine("loc: [" + neuron.Location.Item1 + "," + neuron.Location.Item2 + "," + neuron.Location.Item3 + "]");
                sb.AppendLine("");
            }
            return sb.ToString();
        }

        public string PrintConnectionList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IConnection connection in connections)
            {
                sb.AppendLine(connection.ConnectionDescription());
                sb.AppendLine("mf: " + connection.GetMyelinationFactor.ToString());
                sb.AppendLine("dist: " + connection.GetDistance.ToString());
                sb.AppendLine("round dist: " + connection.GetRoundDistance.ToString());
                sb.AppendLine("");
            }
            sb.AppendLine("Active connections: " + ActiveConnectionCount);
            sb.AppendLine("Rounds to connections shutdown: " + RoundsRemainingForActiveConnectionShutdown);
            return sb.ToString();
        }

        #endregion

        #region Settings

        public int NeuronFireTreshold
        {
            get { return _neuronFireTreshold; }
            set
            {
                _neuronFireTreshold = value;
                foreach (INeuron n in neurons)
                {
                    n.FireTreshold = value;
                }
            }
        }

        public int NeuronReactivationTimeFactor
        {
            get { return _neuronReactivationTimeFactor; }
            set
            {
                _neuronReactivationTimeFactor = value;
                foreach (INeuron n in neurons)
                {
                    n.ReactivationTimeFactor = value;
                }
            }
        }

        public int ConnectionMyelinationGrowthFactor
        {
            get { return _connectionMyelinationGrowthFactor; }
            set
            {
                _connectionMyelinationGrowthFactor = value;
                foreach (IConnection c in connections)
                {
                    c.MyelinationGrowthFactor = value;
                }
            }
        }

        public int ConnectionMyelinationDeclineFactor
        {
            get { return _connectionMyelinationDeclineFactor; }
            set
            {
                _connectionMyelinationDeclineFactor = value;
                foreach (IConnection c in connections)
                {
                    c.MyelinationDeclineFactor = value;
                }
            }
        }

        #endregion

        public long ActiveConnectionCount
        {
            get
            {
                long activeConnectionCount = 0;
                foreach (IConnection c in connections)
                {
                    if (c.IsActive)
                    {
                        activeConnectionCount++;
                    }
                }
                return activeConnectionCount;
            }
        }

        public long RoundsRemainingForActiveConnectionShutdown
        {
            get
            {
                long roundsRemainingForActiveConnectionShutdown = 0;
                foreach (IConnection c in connections)
                {
                    if (c.IsActive)
                    {
                        roundsRemainingForActiveConnectionShutdown += c.GetRemainingRounds;
                    }
                }
                return roundsRemainingForActiveConnectionShutdown;
            }
        }

        public bool IsConnection(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation)
        {
            foreach (IConnection connection in connections)
            {
                if (connection.SourceNeuron.Location == firstLocation && connection.DestinationNeuron.Location == secondLocation)
                    return true;
            }
            return false;
        }

        public double GetDistance(Tuple<int, int, int> firstLocation, Tuple<int, int, int> secondLocation)
        {
            if (IsConnection(firstLocation, secondLocation))
            {
                var connection = from conn in connections
                                 where conn.SourceNeuron.Location == firstLocation
                                 && conn.DestinationNeuron == secondLocation
                                 select conn;
                return connections.FirstOrDefault().GetDistance;

            }
            else
            {
                return -1.0;
            }
        }

        public void Tick()
        {
            logger.Debug("Ticking network");
            IList<INeuron> firedNeurons = new List<INeuron>();
            foreach (INeuron n in neurons)
            {
                n.Tick();
                if (n.IsOutput())
                {
                    firedNeurons.Add(n);
                }
            }
            foreach (IConnection c in connections)
            {
                if (firedNeurons.Contains(c.SourceNeuron))
                {
                    c.Fire();
                }
                c.Tick();
            }
            tickCount++;
            logger.Debug("Network has been ticked (tick no. " + tickCount + ")");
        }
    }
}
