using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsLibrary;

namespace BaseballAverageStatsTest
{
    [TestClass]
    public class BattingAverageTest
    {
        [TestMethod]
        public void DivideByZero()
        {
            double RetVal = BasicStats.BattingAverage(0, 3);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void ValidReturn()
        {
            double RetVal = BasicStats.BattingAverage(3, 1);
            Assert.AreEqual(RetVal, (1 / 3.0), 1E-10);
        }
    }
}
