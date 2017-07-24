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

            Assert.AreEqual(retVals[0].Identifier, "suzukic01");
            Assert.AreEqual(retVals[1].Identifier, "suzukic01");

            Assert.AreEqual(retVals[0].Start, new DateTime(2005, 1, 1));
            Assert.AreEqual(retVals[0].Stop, new DateTime(2005, 12, 31));
            Assert.AreEqual(retVals[1].Start, new DateTime(2006, 1, 1));
            Assert.AreEqual(retVals[1].Stop, new DateTime(2006, 12, 31));

            Assert.AreEqual(retVals[0].Target, StatsTarget.Individual);
            Assert.AreEqual(retVals[1].Target, StatsTarget.Individual);
        }

        public void TestSingleIndividualSingles()
        {
            Singles s = new Singles(database);

            var retVal = s.Compute(new List<string> { "suzukic01" }, StatsTarget.Individual, new DateTime(2005, 1, 1),
                new DateTime(2006, 1, 1));

            var retVals = retVal.ToArray();

            // Should be one result per year requested (2005, 2006)
            Assert.AreEqual(retVals.Length, 2);

            // 
            Assert.AreEqual(retVals[0].Value, 158.0, 1E-3);
            Assert.AreEqual(retVals[1].Value, 186.0, 1E-3);

            Assert.AreEqual(retVals[0].Identifier, "suzukic01");
            Assert.AreEqual(retVals[1].Identifier, "suzukic01");

            Assert.AreEqual(retVals[0].Start, new DateTime(2005, 1, 1));
            Assert.AreEqual(retVals[0].Stop, new DateTime(2005, 1, 1));
            Assert.AreEqual(retVals[1].Start, new DateTime(2006, 1, 1));
            Assert.AreEqual(retVals[1].Stop, new DateTime(2006, 1, 1));

            Assert.AreEqual(retVals[0].Target, StatsTarget.Individual);
            Assert.AreEqual(retVals[1].Target, StatsTarget.Individual);
        }

        [TestMethod]
        public void TestSingleIndividualPlateAppearances()
        {
            PlateAppearances pa = new PlateAppearances(database);

            var retVal = pa.Compute(new List<string> { "suzukic01" }, StatsTarget.Individual, new DateTime(2005, 1, 1), new DateTime(2006, 1, 1));

            var retVals = retVal.ToArray();

            // Should be one result per year requested (2005, 2006)
            Assert.AreEqual(retVals.Length, 2);


            Assert.AreEqual(retVals[0].Value, 739.0, 1E-3);
            Assert.AreEqual(retVals[1].Value, 752.0, 1E-3);

            Assert.AreEqual(retVals[0].Identifier, "suzukic01");
            Assert.AreEqual(retVals[1].Identifier, "suzukic01");

            Assert.AreEqual(retVals[0].Start, new DateTime(2005, 1, 1));
            Assert.AreEqual(retVals[0].Stop, new DateTime(2005, 1, 1));
            Assert.AreEqual(retVals[1].Start, new DateTime(2006, 1, 1));
            Assert.AreEqual(retVals[1].Stop, new DateTime(2006, 1, 1));

            Assert.AreEqual(retVals[0].Target, StatsTarget.Individual);
            Assert.AreEqual(retVals[1].Target, StatsTarget.Individual);
        }

        [TestMethod]
        public void TestSingleTeamBA()
        {
            BattingAverage ba = new BattingAverage(database);

            var retVal = ba.Compute(new List<string> {"SEA"}, StatsTarget.Team, new DateTime(2006, 1, 1), new DateTime(2006, 12, 31));

            var retVals = retVal.ToArray();

            Assert.AreEqual(retVals.Length, 1);
            Assert.AreEqual(retVals[0].Target, StatsTarget.Team);
            Assert.AreEqual(retVals[0].Identifier, "SEA");
            Assert.AreEqual(retVals[0].Start, new DateTime(2006, 1, 1));
            Assert.AreEqual(retVals[0].Stop, new DateTime(2006, 12, 31));
            Assert.AreEqual(retVals[0].Value, 0.271, 1E-3);
        }
    }
}
