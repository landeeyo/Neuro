using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NeuroCore.Tests
{
    [TestFixture]
    public class DistanceTest
    {
        INeuralNetwork nn;
        SimpleNeuron n1, n2;
        IConnection c1;

        [SetUp]
        public void Setup()
        {
            nn = new SimpleNeuralNetwork();
        }

        [TearDown]
        public void TearDown()
        {
            nn = null;
        }

        [Test]
        public void TestOne()
        {
            #region Adding neurons

            n1 = new SimpleNeuron(new Tuple<int, int, int>(1, 1, 1));
            n2 = new SimpleNeuron(new Tuple<int, int, int>(10, 10, 10));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);

            #endregion

            #region Connecting neurons

            c1 = new SimpleConnection(n1, n2);
            nn.AddConnection(c1);

            #endregion

            double result = nn.GetDistance(n1.Location, n2.Location);
            double properResult = 15.5884;
            Assert.AreEqual(properResult, result, 0.4);

            #region Cleanup

            nn.RemoveNeuron(n1);
            nn.RemoveNeuron(n2);
            nn.RemoveConnection(c1);

            #endregion
        }

        [Test]
        public void TestTwo()
        {
            #region Adding neurons

            n1 = new SimpleNeuron(new Tuple<int, int, int>(5, 10, 2));
            n2 = new SimpleNeuron(new Tuple<int, int, int>(13, 9, 27));

            nn.AddNeuron(n1);
            nn.AddNeuron(n2);

            #endregion

            #region Connecting neurons

            c1 = new SimpleConnection(n1, n2);
            nn.AddConnection(c1);

            #endregion

            double result = nn.GetDistance(n1.Location, n2.Location);
            double properResult = 26.2678;
            Assert.AreEqual(properResult, result, 0.4);

            #region Cleanup

            nn.RemoveNeuron(n1);
            nn.RemoveNeuron(n2);
            nn.RemoveConnection(c1);

            #endregion
        }
    }
}
