using System;
using System.Collections.Generic;
using System.Linq;
using LahmanStats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsManager;
using WpfApplication1;

namespace LahmanStatsTests
{
    [TestClass]
    public class UnitTest1
    {
        private static LahmanEntities database;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            database = new LahmanEntities();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            database.Dispose();
        }

        [TestMethod]
        public void TestSingleIndividualBA()
        {
            BattingAverage ba = new BattingAverage(database);

            var retVal = ba.Compute(new List<string> {"suzukic01"}, StatsTarget.Individual, new DateTime(2005, 1, 1),
                new DateTime(2006, 1, 1));

            var retVals = retVal.ToArray();

            // Should be one result per year requested (2005, 2006)
            Assert.AreEqual(retVals.Length, 2);

            // Note relaxed spec on equality.  Just checking BA meets expectation to within 3 digits.
            // Good enough to prove that I'm pulling the correct data for the given years
            Assert.AreEqual(retVals[0].Value, 0.303, 1E-3);
            Assert.AreEqual(retVals[1].Value, 0.322, 1E-3);
        }
    }
}
