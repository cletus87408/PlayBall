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

    [TestClass]
    public class AtBatsPerHomeRunTest
    {
        [TestMethod]
        public void AtBatsPerHomeRunDivideByZero()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void AtBatsPerHomeRunValidReturn()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3, 1);
            Assert.AreEqual(RetVal, 3.0, 1E-10);
        }
        [TestMethod]
        public void AtBatsPerHomeRunValidReturnLarge()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3000, 250);
            Assert.AreEqual(RetVal, (3000/250.0), 1E-10);
        }
    }

    [TestClass]
    public class WalkToStrikeoutTest
    {
        [TestMethod]
        public void WalkToStrikeoutDivideByZero()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void WalkToStrikeoutValid()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(2, 1);
            Assert.AreEqual(RetVal, 2.0, 1E-10);
        }

        [TestMethod]
        public void WalkToStrikeoutValidLarge()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(2000, 500);
            Assert.AreEqual(RetVal, 4.0, 1E-10);
        }
    }
    [TestClass]
    public class PlateAppearancesPerStrikeoutTest
    {
        [TestMethod]
        public void PlateAppearancesPerStrikeoutDivideByZero()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void PlateAppearancesPerStrikeoutValid()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(2, 1);
            Assert.AreEqual(RetVal, 2.0, 1E-10);
        }

        [TestMethod]
        public void PlateAppearancesPerStrikeoutValidLarge()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(3000, 500);
            Assert.AreEqual(RetVal, 6.0, 1E-10);
        }
    }
    [TestClass]
    public class TBStatsTest
    {
        [TestMethod]
        public void TBValid()
        {
            double RetVal = BasicStats.TotalBases(1, 1, 1, 1);
            Assert.AreEqual(RetVal, 10.0, 1E-10);
        }

        [TestMethod]
        public void TBValidLarge()
        {
            double RetVal = BasicStats.TotalBases(10, 10, 10, 10);
            Assert.AreEqual(RetVal, 100.0, 1E-10);
        }
    }

    [TestClass]
    public class RPStatsTest
    {
        [TestMethod]
        public void RPValid()
        {
            double RetVal = BasicStats.RunsProduced(1, 1, 1);
            Assert.AreEqual(RetVal, 1.0, 1E-10);
        }

        [TestMethod]
        public void RPValidLarge()
        {
            double RetVal = BasicStats.RunsProduced(100, 100, 50);
            Assert.AreEqual(RetVal, 150.0, 1E-10);
        }
    }

    [TestClass]
    public class SLGStatsTest
    {
        [TestMethod]
        public void SLGDivideByZero()
        {
            double RetVal = BasicStats.SluggingPercentage(1, 1, 1, 1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void SLGValid()
        {
            double RetVal = BasicStats.SluggingPercentage(1, 1, 1, 1, 8);
            Assert.AreEqual(RetVal, (10 / 8.0), 1E-10);
        }

        [TestMethod]
        public void SLGValidLarge()
        {
            double RetVal = BasicStats.SluggingPercentage(10, 10, 10, 10, 200);
            Assert.AreEqual(RetVal, (100 / 200.0), 1E-10);
        }
    }

    [TestClass]
    public class TAStatsTest
    {
        [TestMethod]
        public void TADivideByZero()
        {
            double RetVal = BasicStats.TotalAverage(1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void TAValid()
        {
            double RetVal = BasicStats.TotalAverage(1, 1, 1, 1, 1, 1, 1, 8, 4, 1, 1);
            Assert.AreEqual(RetVal, (13 / 6.0), 1E-10);         
        }

        [TestMethod]
        public void TAValid_Large()
        {
            double RetVal = BasicStats.TotalAverage(10, 10, 10, 10, 10, 10, 10, 400, 40, 10, 5);
            Assert.AreEqual(RetVal, (130 / 375.0), 1E-10);
        }

    }

    [TestClass]
    public class TimesOnBaseStatsTest
    {
        [TestMethod]
        public void TOBValid()
        {
            double RetVal = BasicStats.TimesOnBase(1, 1, 1);
            Assert.AreEqual(RetVal, 3.0, 1E-10);
        }

        [TestMethod]
        public void TOBValidLarge()
        {
            double RetVal = BasicStats.TimesOnBase(1000, 500, 50);
            Assert.AreEqual(RetVal, 1550.0, 1E-10);
        }
    }
    
    [TestClass]
    public class XBHStatsTest
    {
        [TestMethod]
        public void XBHValid()
        {
            double RetVal = BasicStats.ExtraBaseHits(1, 1, 1);
            Assert.AreEqual(RetVal, 3.0, 1E-10);
        }

        [TestMethod]
        public void XBHValidLarge()
        {
            double RetVal = BasicStats.ExtraBaseHits(200, 50, 100);
            Assert.AreEqual(RetVal, 350.0, 1E-10);
        }
    }

    [TestClass]
    public class StolenBasePercentage
    {
        [TestMethod]
        public void StolenBasePercentageDivideByZero()
        {
            double RetVal = BasicStats.StolenBasePercentage(1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void StolenBasePercentageValid()
        {
            double RetVal = BasicStats.StolenBasePercentage(2, 4);
            Assert.AreEqual(RetVal, (2 / 4.0), 1E-10);
        }

        [TestMethod]
        public void StolenBasePercentageValidLarge()
        {
            double RetVal = BasicStats.StolenBasePercentage(70, 300);
            Assert.AreEqual(RetVal, (70 / 300.0), 1E-10);
        }
    }

    [TestClass]
    public class TotalChancesStatTest
    {
        [TestMethod]
        public void TotalChancesValid()
        {
            double RetVal = BasicStats.TotalChances(1, 1, 1);
            Assert.AreEqual(RetVal, 3.0, 1E-10);
        }
        public void TotalChancesValidLarge() //used ichiro 2003
        {
            double RetVal = BasicStats.TotalChances(12, 337, 2);
            Assert.AreEqual(RetVal, (12.0 + 337 + 2), 1E-10);
        }
    }
    [TestClass]
    public class FieldingPercentageStatTest
    {
        [TestMethod]
        public void FieldingPercentageDividByZero()
        {
            double RetVal = BasicStats.FieldingPercentage(0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void FieldingPercentageValid()
        {
            double RetVal = BasicStats.FieldingPercentage(2, 2, 1);
            Assert.AreEqual(RetVal, ((5.0 - 1) / 5), 1E-10);
        }

        [TestMethod]
        public void FieldingPercentageValidLarge() //Used Ichiro 2003 real data
        {
            double RetVal = BasicStats.FieldingPercentage(12,337,2);
            Assert.AreEqual(RetVal, ((351.0 - 2) / 351), 1E-10);
        }

    }
    
    [TestClass]
    public class RangeFactorStatTest
    {
        [TestMethod]
        public void RangeFactorDivideByZero()
        {
            double RetVal = BasicStats.RangeFactor(1, 1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void RangeFactorValid()
        {
            double RetVal = BasicStats.RangeFactor(1, 1, 1);
            Assert.AreEqual(RetVal, 18.0, 1E-10);
        }

        [TestMethod]
        public void RangeFactorLarge() //used ichiro 2003 data
        {
            double RetVal = BasicStats.RangeFactor(337, 12, 1367);
            Assert.AreEqual(RetVal, ((9.0*(337+12))/1367), 1E-10);
        }
    }

    [TestClass]
    public class BABIPStatsTest
    {
        [TestMethod]
        public void BABIPDivideByZero()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(1, 0, 1, 1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void BABIPValidReturn()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(2, 0, 4, 1, 2);
            Assert.AreEqual(RetVal, (2 / 5.0), 1E-10);
        }

        [TestMethod]
        public void BABIPValidReturnLarge()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(200, 24, 400, 30, 5);
            Assert.AreEqual(RetVal, (176 / 351.0), 1E-10);
        }
    }

    [TestClass]
    public class RCStatsTest
    {
        [TestMethod]
        public void RCDivideByZero()
        {
            double RetVal = AdvancedStats.RunsCreated(1, 1, 1, 1, 0, 0, 1);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void RCValid()
        {
            double RetVal = AdvancedStats.RunsCreated(1, 1, 1, 1, 1 , 8, 4);
            Assert.AreEqual(RetVal, (50 / 9.0), 1E-10);
        }

        [TestMethod]
        public void RCValidLarge()
        {
            double RetVal = AdvancedStats.RunsCreated(10, 10, 10, 10, 10, 80, 40);
            Assert.AreEqual(RetVal, (5000 / 90.0), 1E-10);
        }
    }

    [TestClass]
    public class XRStatTest
    {
        [TestMethod]
        public void XRValid()
        {
            double RetVal = AdvancedStats.ExtrapolatedRuns(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, 4.182, 1E-10);
        }

        [TestMethod]
        public void XRValidLarge() //test with Mark McGwire's 1998 data
        {
            double RetVal = AdvancedStats.ExtrapolatedRuns(61, 21, 0, 70, 6, 162, 28, 1, 0, 509, 152, 155, 8, 4, 0);
            Assert.AreEqual(RetVal, 166.35, 1E-10);
        }
    }

    [TestClass]
    public class EquivalentAverageStatTest
    {
        [TestMethod]
        public void EqADivideByZero()
        {
            double RetVal = AdvancedStats.EquivalentAverage(4, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }
        
        [TestMethod]
        public void EqAValid()
        {
            double RetVal = AdvancedStats.EquivalentAverage(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, (17.0 / (19 / 3.0)), 1E-10);
        }

        [TestMethod]
        public void EqAValidLarge() //used Ichiro 2003
        {
            double RetVal = AdvancedStats.EquivalentAverage(212, 162, 29, 8, 13, 36, 6, 34, 3, 1, 679, 8);
            Assert.AreEqual(RetVal, ((212+296+1.5*(36+6)+34+3+1.0)/(679+36+6+3+1+8+(34/3.0))), 1E-10);
        }
    }

    [TestClass]
    public class EarnedRunAverageStatTest
    {
        [TestMethod]
        public void ERADivideByZero()
        {
            double RetVal = PitchingStats.EarnedRunAverage(1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void ERAValid()
        {
            double RetVal = PitchingStats.EarnedRunAverage(1, 9);
            Assert.AreEqual(RetVal, 1.0, 1E-10);
        }

        [TestMethod]
        public void ERAValidLarge() //used randy johnson 1995
        {
            double RetVal = PitchingStats.EarnedRunAverage(65, 214.1);
            Assert.AreEqual(RetVal, ((9.0 * 65) / 214.1), 1E-10);
        }
    }

    [TestClass]
    public class BaseOnBallsPerNineInningsStatTest
    {
        [TestMethod]
        public void BaseOnBallsPerNineInningsValid()
        {
            double RetVal = PitchingStats.BaseOnBallsPerNineInnings(9);
            Assert.AreEqual(RetVal, 1.0, 1E-10);
        }
        [TestMethod]
        public void BaseOnBallsPerNineInningsValidLarge() //using randy johnson 1995
        {
            double RetVal = PitchingStats.BaseOnBallsPerNineInnings(65);
            Assert.AreEqual(RetVal, (65 / 9.0), 1E-10);
        }
    }
}
