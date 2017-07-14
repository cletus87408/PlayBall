using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsLibrary;

namespace BaseballAverageStatsTest
{
    [TestClass]
    public class SinglesStatsTest
    {
        // Singles = H - 2B - 3B - HR
        [TestMethod]
        public void SinglesValid()
        {
            double RetVal = BasicStats.Singles(8, 1, 1, 1);
            Assert.AreEqual(RetVal, (8.0 - 1 - 1 - 1), 1E-10);
        }

        [TestMethod]
        public void SinglesValidLarge() //using ichiro 2003
        {
            double RetVal = BasicStats.Singles(212, 29, 8, 13);
            Assert.AreEqual(RetVal, (212.0 - 29 - 8 - 13), 1E-10);
        }
    }

    [TestClass]
    public class PlateAppearancesStatsTest
    {
        //PA = AB + BB + HBP + SH + SF + Times Reached on Defensive Interference
        [TestMethod]
        public void PlateAppearancesValid()
        {
            double RetVal = BasicStats.PlateAppearances(1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + 1 + 1 + 1 + 1 + 1), 1E-10);
        }

        [TestMethod]
        public void PlateAppearancesValidLarge() //used ichiro 2003
        {
            double RetVal = BasicStats.PlateAppearances(679, 36, 6, 3, 1, 0);
            Assert.AreEqual(RetVal, (679.0 + 36 + 6 + 3 + 1 + 0), 1E-10);
        }
    }

    [TestClass]
    public class NonIntentionalBaseOnBalls
    {
        //Non Intentional Base on Balls = BB - IBB
        [TestMethod]
        public void NonIntentionalBaseOnBallsValid()
        {
            double RetVal = BasicStats.NonIntentionalBaseOnBalls(2, 1);
            Assert.AreEqual(RetVal, (2.0 - 1), 1E-10);
        }

        [TestMethod]
        public void NonIntentionalBaseOnBallsValidLarge()
        {
            double RetVal = BasicStats.NonIntentionalBaseOnBalls(300, 4);
            Assert.AreEqual(RetVal, (300.0 - 4), 1E-10);
        }
    }

    [TestClass]
    public class BattingAverageStatsTest
    {
        //Batting Average = H / AB
        [TestMethod]
        public void BattingAverageDivideByZero()
        {
            double RetVal = BasicStats.BattingAverage(0, 3);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void BattingAverageValid()
        {
            double RetVal = BasicStats.BattingAverage(3, 1);
            Assert.AreEqual(RetVal, (1 / 3.0), 1E-10);
        }

        [TestMethod]
        public void BattingAverageValidLarge() //used Ichiro 2003
        {
            double RetVal = BasicStats.BattingAverage(679, 212);
            Assert.AreEqual(RetVal, (212.0 / 679), 1E-10);
        }
    }

    [TestClass]
    public class AtBatsPerHomeRunStatsTest
    {
        // At Bats Per Homerun = AB / HR

        [TestMethod]
        public void AtBatsPerHomeRunDivideByZero()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void AtBatsPerHomeRunValidReturn()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3, 1);
            Assert.AreEqual(RetVal, (3.0 / 1), 1E-10);
        }
        [TestMethod]
        public void AtBatsPerHomeRunValidReturnLarge()
        {
            double RetVal = BasicStats.AtBatsPerHomeRun(3000, 250);
            Assert.AreEqual(RetVal, (3000 / 250.0), 1E-10);
        }
    }

    [TestClass]
    public class WalkToStrikeoutStatsTest
    {
        //Walks to Strikeouts = BB / K

        [TestMethod]
        public void WalkToStrikeoutDivideByZero()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void WalkToStrikeoutValid()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(2, 1);
            Assert.AreEqual(RetVal, (2.0 / 1), 1E-10);
        }

        [TestMethod]
        public void WalkToStrikeoutValidLarge()
        {
            double RetVal = BasicStats.WalkToStrikeoutRatio(2000, 500);
            Assert.AreEqual(RetVal, (2000.0 / 500), 1E-10);
        }
    }
    [TestClass]
    public class PlateAppearancesPerStrikeoutStatsTest
    {
        //Plate Appearances Per Strikeout = PA / K
        //PA = AB + BB + HBP + SH + SF + Times Reached on Defensive Interference

        [TestMethod]
        public void PlateAppearancesPerStrikeoutDivideByZero()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(1, 1, 1, 1, 1, 1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void PlateAppearancesPerStrikeoutValid()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(1, 1, 1, 1, 1, 0, 1);
            Assert.AreEqual(RetVal, ((1.0 + 1 + 1 + 1 + 1 + 0) / 1), 1E-10);
        }

        [TestMethod]
        public void PlateAppearancesPerStrikeoutValidLarge()
        {
            double RetVal = BasicStats.PlateAppearancesPerStrikeout(500, 150, 10, 5, 10, 1, 500);
            Assert.AreEqual(RetVal, ((500.0 + 150 + 10 + 5 + 10 + 1) / 500), 1E-10);
        }
    }
    [TestClass]
    public class TBStatsTest
    {
        //Total Bases = (1B + (2B * 2) + (3B *3) + (HR * 4))

        [TestMethod]
        public void TBValid()
        {
            double RetVal = BasicStats.TotalBases(4, 1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + (1 * 2) + (1 * 3) + (1 * 4)), 1E-10);
        }

        [TestMethod]
        public void TBValidLarge()
        {
            double RetVal = BasicStats.TotalBases(40, 10, 10, 10);
            Assert.AreEqual(RetVal, (10.0 + (10 * 2) + (10 * 3) + (10 * 4)), 1E-10);
        }
    }

    [TestClass]
    public class RPStatsTest
    {
        // Runs Produced = R + RBI - HR

        [TestMethod]
        public void RPValid()
        {
            double RetVal = BasicStats.RunsProduced(1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + 1 - 1), 1E-10);
        }

        [TestMethod]
        public void RPValidLarge()
        {
            double RetVal = BasicStats.RunsProduced(100, 100, 50);
            Assert.AreEqual(RetVal, (100.0 + 100 - 50), 1E-10);
        }
    }

    [TestClass]
    public class SLGStatsTest
    {
        //Slugging Average = ((1B + (2B * 2) + (3B * 3) + (4B * 4))/AB)

        [TestMethod]
        public void SLGDivideByZero()
        {
            double RetVal = BasicStats.SluggingPercentage(1, 1, 1, 1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void SLGValid()
        {
            double RetVal = BasicStats.SluggingPercentage(4, 1, 1, 1, 8);
            Assert.AreEqual(RetVal, ((1 + (1 * 2) + (1 * 3) + (1 * 4)) / 8.0), 1E-10);
        }

        [TestMethod]
        public void SLGValidLarge()
        {
            double RetVal = BasicStats.SluggingPercentage(40, 10, 10, 10, 200);
            Assert.AreEqual(RetVal, ((10 + (10 * 2) + (10 * 3) + (10 * 4)) / 200.0), 1E-10);
        }
    }

    [TestClass]
    public class TAStatsTest
    {
        //Total Average = ((TB + BB + HBP + SB - CS) / (AB - H + CS + GIDP))

        [TestMethod]
        public void TADivideByZero()
        {
            double RetVal = BasicStats.TotalAverage(1, 1, 1, 1, 1, 1, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void TAValid()
        {
            double RetVal = BasicStats.TotalAverage(1, 1, 1, 1, 1, 1, 8, 4, 1, 1);
            Assert.AreEqual(RetVal, ((10 + 1 + 1 + 1 - 1) / (8 - 4 + 1 + 1.0)), 1E-10);
        }

        [TestMethod]
        public void TAValid_Large()
        {
            double RetVal = BasicStats.TotalAverage(10, 10, 10, 10, 10, 10, 400, 40, 10, 5);
            Assert.AreEqual(RetVal, ((100 + 10 + 10 + 10 - 10) / (400 - 40 + 10 + 5.0)), 1E-10);
        }

    }

    [TestClass]
    public class TimesOnBaseStatsTest
    {
        //Times On Base = H + BB + HBP

        [TestMethod]
        public void TOBValid()
        {
            double RetVal = BasicStats.TimesOnBase(1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + 1 +1), 1E-10);
        }

        [TestMethod]
        public void TOBValidLarge()
        {
            double RetVal = BasicStats.TimesOnBase(1000, 500, 50);
            Assert.AreEqual(RetVal, (1000.0 + 500 + 50), 1E-10);
        }
    }

    [TestClass]
    public class XBHStatsTest
    {
        //Extra Base Hits = (2B + 3B + HR)

        [TestMethod]
        public void XBHValid()
        {
            double RetVal = BasicStats.ExtraBaseHits(1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + 1 + 1), 1E-10);
        }

        [TestMethod]
        public void XBHValidLarge()
        {
            double RetVal = BasicStats.ExtraBaseHits(200, 50, 100);
            Assert.AreEqual(RetVal, (200.0 + 50 + 100), 1E-10);
        }
    }

    [TestClass]
    public class OnBasePercentageStatsTest
    {
        //On Base Percentage = (H + BB + HBP)/(AB + BB + HBP + SF)

        [TestMethod]
        public void OBPDivideByZero()
        {
            double RetVal = BasicStats.OnBasePercentage(1, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void OBPValid()
        {
            double RetVal = BasicStats.OnBasePercentage(1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, ((1 + 1 + 1) / (1.0 * (1 + 1 + 1 + 1))), 1E-10);
        }

        [TestMethod]
        public void OBPValidLarge() //using ichiro 2003
        {
            double RetVal = BasicStats.OnBasePercentage(212, 36, 6, 679, 1);
            Assert.AreEqual(RetVal, ((212 + 36 + 6) / (679 + 36 + 6 + 1.0)), 1E-10);
        }
    }

    [TestClass]
    public class OnBasePlusSluggingStatsTest
    {
        //On Base Plus Slugging = Slugging Percentage + On Base Percentage
        //((1B + (2B * 2) + (3B * 3) + (4B * 4))/AB) + ((H + BB + HBP)/(AB + BB + HBP + SF))  

        [TestMethod]
        public void OnBasePlusSluggingValid()
        {
            double RetVal = BasicStats.OnBasePlusSlugging(4, 1, 1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, ((1.0 + 2 + 3 + 4) / 1) + ((4 + 1 + 1) / (1 + 1 + 1 + 1.0)), 1E-10);
        }

        [TestMethod]
        public void OnBasePlusSluggingValidLarge() //used ichiro 2003
        {
            double RetVal = BasicStats.OnBasePlusSlugging(212, 36, 6, 679, 1, 29, 8, 13);
            Assert.AreEqual(RetVal, ((162 + (29 * 2.0) + (8 * 3.0) + (13 * 4.0)) / 679.0) + ((212 + 36 + 6) / (679 + 36 + 6 + 1.0)), 1E-10);
        }
    }
    [TestClass]
    public class StolenBasePercentage
    {
        //Stolen Base Percentage = Stolen Bases/ Attempts

        [TestMethod]
        public void StolenBasePercentageDivideByZero()
        {
            double RetVal = BasicStats.StolenBasePercentage(1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
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
        //Total Chances = Assists + Putouts + Errors 

        [TestMethod]
        public void TotalChancesValid()
        {
            double RetVal = BasicStats.TotalChances(1, 1, 1);
            Assert.AreEqual(RetVal, (1.0 + 1 + 1), 1E-10);
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
        //Fielding Percentage = (Chances - Errors) / Total Chances

        [TestMethod]
        public void FieldingPercentageDividByZero()
        {
            double RetVal = BasicStats.FieldingPercentage(0, 0, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
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
            double RetVal = BasicStats.FieldingPercentage(12, 337, 2);
            Assert.AreEqual(RetVal, ((351.0 - 2) / 351), 1E-10);
        }

    }

    [TestClass]
    public class RangeFactorStatTest
    {
        //Range Factor = 9 * (putouts + assists) / innings played

        [TestMethod]
        public void RangeFactorDivideByZero()
        {
            double RetVal = BasicStats.RangeFactor(1, 1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void RangeFactorValid()
        {
            double RetVal = BasicStats.RangeFactor(1, 1, 1);
            Assert.AreEqual(RetVal, ((9.0 * (1 + 1)) / 1), 1E-10);
        }

        [TestMethod]
        public void RangeFactorLarge() //used ichiro 2003 data
        {
            double RetVal = BasicStats.RangeFactor(337, 12, 1367);
            Assert.AreEqual(RetVal, ((9.0 * (337 + 12)) / 1367), 1E-10);
        }
    }

    [TestClass]
    public class BABIPStatsTest
    {
        //Batting Average On Balls In Play = ((H - HR) / (AB - K - HR +SF))

        [TestMethod]
        public void BABIPDivideByZero()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(1, 0, 1, 1, 0);
            Assert.AreEqual(RetVal, 0);  //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void BABIPValidReturn()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(2, 0, 4, 1, 2);
            Assert.AreEqual(RetVal, ((2.0 - 0) / (4 - 1 - 0 + 2)), 1E-10);
        }

        [TestMethod]
        public void BABIPValidReturnLarge()
        {
            double RetVal = AdvancedStats.BattingAverageOnBallsInPlay(200, 24, 400, 30, 5);
            Assert.AreEqual(RetVal, ((200 - 24.0) / (400 - 30 - 24 + 5)), 1E-10);
        }
    }

    [TestClass]
    public class RCStatsTest
    {
        //Runs Created = ((H + BB) * TB)/ (AB + BB)

        [TestMethod]
        public void RCDivideByZero()
        {
            double RetVal = AdvancedStats.RunsCreated(1, 1, 1, 0, 0, 1);
            Assert.AreEqual(RetVal, 0); //function returns 0.0 for divide by zero
        }

        [TestMethod]
        public void RCValid()
        {
            double RetVal = AdvancedStats.RunsCreated(1, 1, 1, 1, 8, 4);
            Assert.AreEqual(RetVal, ((4 + 1) * 10) / (8 + 1.0), 1E-10);
        }

        [TestMethod]
        public void RCValidLarge()
        {
            double RetVal = AdvancedStats.RunsCreated(10, 10, 10, 10, 80, 40);
            Assert.AreEqual(RetVal, (((40 + 10) * 100) / (80 + 10.0)), 1E-10);
        }
    }

    [TestClass]
    public class XRStatsTest
    {
        //Extrapolated Runs = (.50 * 1B) + (.72 * 2B) + (1.04 * 3B) + (1.44 * HR) + (.34 * (HBP+TBB-IBB)) + (.25 * IBB) + (.18 * SB) + (-.32 * CS) + (-.090 * (AB-H-K)) +(-.098 * K) + (-.37 * GIDP) + (.37 * SF) + (.04 * SH)

        [TestMethod]
        public void XRValid()
        {
            double RetVal = AdvancedStats.ExtrapolatedRuns(1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, ((.50 * 1) + (.72 * 1) + (1.04 * 1) + (1.44 * 1) + (.34 * (1+1-1)) + (.25 * 1) + (.18 * 1) + (-.32 * 1) +( -.090 * (1-4-1)) + (-.098 * 1) + (-.37 * 1) + (.37 * 1) + (.04 * 1)), 1E-10);
        }

        [TestMethod]
        public void XRValidLarge() //test with Mark McGwire's 1998 data
        {
            double RetVal = AdvancedStats.ExtrapolatedRuns(21, 0, 70, 6, 162, 28, 1, 0, 509, 152, 155, 8, 4, 0);
            Assert.AreEqual(RetVal, ((.50 * 61) + (.72 * 21) + (1.04 * 0) + (1.44 * 70) + (.34 * (6 + 162 - 28)) + (.25 * 28) + (.18 * 1) + (-.32 * 0) + (-.090 * (509 - 152 - 155)) + (-.098 * 155) + (-.37 * 8) + (.37 * 4) + (.04 * 0)), 1E-10);
        }
    }

    [TestClass]
    public class EquivalentAverageStatsTest
    {
        //Equivalent Average = (Hits + Total Bases + 1.5 * (Walks + Hit By Pitch) + Stolen Bases + Sacrifice Hits + Sacrifice Flies) / (At bats + Walks + Hit By Pitch + Sac Hits + Sac Flies + Caught Stealing + (Stolen Bases / 3) )

        [TestMethod]
        public void EqADivideByZero()
        {
            double RetVal = AdvancedStats.EquivalentAverage(4, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void EqAValid()
        {
            double RetVal = AdvancedStats.EquivalentAverage(4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, ((4 + 10 + (1.5) * (1 + 1) + 1 + 1 + 1) / (1 + 1 + 1 + 1 + 1 + 1 + (1.0/3) )), 1E-10);
        }

        [TestMethod]
        public void EqAValidLarge() //used Ichiro 2003
        {
            double RetVal = AdvancedStats.EquivalentAverage(212, 29, 8, 13, 36, 6, 34, 3, 1, 679, 8);
            Assert.AreEqual(RetVal, ((212 + 296 + 1.5 * (36 + 6) + 34 + 3 + 1.0) / (679 + 36 + 6 + 3 + 1 + 8 + (34 / 3.0))), 1E-10);
        }
    }

    [TestClass]
    public class BaseRunsStatsTest
    {
        //BsR (Base Runs) = (A*B)/(B+C)+D
        //where A= H + BB - HR, B = (1.4 * TB - 0.6 * H - 3 * HR + 0.1 * BB) * 1.02, C = AB - H, D = HR

        [TestMethod]
        public void BaseRunsDivideByZero()
        {
            double RetVal = AdvancedStats.BaseRuns(0, 0, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void BaseRunsValid()
        {
            double totalBases = BasicStats.TotalBases(4, 1, 1, 1);
            double RetVal = AdvancedStats.BaseRuns(4, 1, 1, 1, 1, 8);
            int A = (4 + 1 - 1); //H + BB - HR
            int C = (8 - 4); //AB - H
            double B = ((1.4 * totalBases - 0.6 * 4 - 3 * 1 + 0.1 * 1) *1.02); //(1.4 * TB - 0.6 * H - 3 * HR + 0.1 * BB) * 1.02

            Assert.AreEqual(RetVal, (((A * B) / (B + C)) + 1.0), 1E-10); //BR (Base Runs) = (A*B)/(B+C) + HR
        }

        [TestMethod]
        public void BaseRunsValidLarge() //using ichiro 2003
        {
            double totalBases = BasicStats.TotalBases(212, 29, 8, 13);
            double RetVal = AdvancedStats.BaseRuns(212, 36, 13, 29, 8, 679);
            int A = (212 + 36 - 13); //H + BB - HR
            int C = (679 - 212); //AB - H
            double B = ((1.4 * totalBases - 0.6 * 212 - 3 * 13 + 0.1 * 36) * 1.02); //(1.4 * TB - 0.6 * H - 3 * HR + 0.1 * BB) * 1.02

            Assert.AreEqual(RetVal, (((A * B) / (B + C)) + 13.0), 1E-10); //BR (Base Runs) = (A*B)/(B+C) + HR
        }
    }

    [TestClass]
    public class GrossProductionAverageStatsTest
    {
        // Gross Production Average = (1.8 * OPS) / 4

        [TestMethod]
        public void GrossProductionAverageValid()
        {
            double RetVal = AdvancedStats.GrossProductionAverage(1, 1, 1, 1, 1, 1, 1, 1);
            double OPS = BasicStats.OnBasePlusSlugging(1, 1, 1, 1, 1, 1, 1, 1);
            Assert.AreEqual(RetVal, ((1.8 * OPS) / 4), 1E-10);
        }
    }

    [TestClass]
    public class WeightedOnBaseAverageStatsTest
    {
        // Weighted on base average = ((0.72 * NIBB) + (0.75 * HBP) + (0.90 * 1B) + (0.92 * RBOE) + (1.24 * 2B) + (1.56 * 3B) + (1.95 * HR)) / PA
        [TestMethod]
        public void WeightedOnBaseAverageDivideByZero()
        {
            double RetVal = AdvancedStats.WeightedOnBaseAverage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void WeightedOnBaseAverageValid() //using ichiro 2003
        {
            double RetVal = AdvancedStats.WeightedOnBaseAverage(36, 7, 6, 212, 0, 29, 8, 13, 679, 3, 1, 0);

            double plateAppearances = BasicStats.PlateAppearances(679, 36, 6, 3, 1, 0);
            double singles = BasicStats.Singles(212, 29, 8, 13);
            double nonIntentionalWalks = BasicStats.NonIntentionalBaseOnBalls(36, 7);
            double wOBA = ((0.72 * nonIntentionalWalks) + (0.75 * 6) + (0.90 * singles) + (0.92 * 0) + (1.24 * 29) + (1.56 * 8) + (1.95 * 13)) / plateAppearances;

            Assert.AreEqual(RetVal, wOBA, 1E-10);
        }
    }

    [TestClass]
    public class WeightedRunsAboveAverage
    {
        // Weighted Runs Above Average = ((wOBA – lgwOBA)/wOBA Scale) * PA
        [TestMethod]
        public void WeightedRunsAboveAverageDivideByZero()
        {
            double RetVal = AdvancedStats.WeightedRunsAboveAverage(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void WeightedRunsAboveAverageValid()
        {
            double RetVal = AdvancedStats.WeightedRunsAboveAverage(.100, .200, 2, 1, 1, 4, 0, 1, 1, 1, 8, 1, 1, 0);
            double weightedOnBaseAverage = AdvancedStats.WeightedOnBaseAverage(2, 1, 1, 4, 0, 1, 1, 1, 8, 1, 1, 0);
            double plateAppearances = BasicStats.PlateAppearances(8, 2, 1, 1, 1, 0);

            Assert.AreEqual(RetVal, (((weightedOnBaseAverage - .100) / .200) * plateAppearances), 1E-10);
        }

        [TestMethod]
        public void WeightedRunsAboveAverageValidLarge()
        {
            double RetVal = AdvancedStats.WeightedRunsAboveAverage(.450, 4, 50, 4, 5, 200, 4, 20, 5, 20, 700, 4, 2, 2);
            double weightedOnBaseAverage = AdvancedStats.WeightedOnBaseAverage(50, 4, 5, 200, 4, 20, 5, 20, 700, 4, 2, 2);
            double plateAppearances = BasicStats.PlateAppearances(700, 50, 5, 4, 2, 2);

            Assert.AreEqual(RetVal, (((weightedOnBaseAverage - .450) / 4) * plateAppearances), 1E-10);
        }
    }

    [TestClass]
    public class EarnedRunAverageStatsTest
    {
        // Earned Run Average = Earned Runs * 9 / Innings

        [TestMethod]
        public void ERADivideByZero()
        {
            double RetVal = PitchingStats.EarnedRunAverage(1, 0);
            Assert.AreEqual(RetVal, 0); //function returns 0.0 for divide by zero
        }

        [TestMethod]
        public void ERAValid()
        {
            double RetVal = PitchingStats.EarnedRunAverage(1, 9);
            Assert.AreEqual(RetVal, ((9.0 * 1) / 9), 1E-10); 
        }

        [TestMethod]
        public void ERAValidLarge() //used randy johnson 1995
        {
            double RetVal = PitchingStats.EarnedRunAverage(65, 214.1);
            Assert.AreEqual(RetVal, ((9.0 * 65) / 214.1), 1E-10); 
        }
    }

    [TestClass]
    public class BaseOnBallsPerNineInningsStatsTest
    {
        // Base On Balls Per Nine Innings = BB / 9 innings

        [TestMethod]
        public void BaseOnBallsPerNineInningsValid()
        {
            double RetVal = PitchingStats.BaseOnBallsPerNineInnings(9);
            Assert.AreEqual(RetVal, (9.0 / 9), 1E-10); 
        }
        [TestMethod]
        public void BaseOnBallsPerNineInningsValidLarge() //using randy johnson 1995
        {
            double RetVal = PitchingStats.BaseOnBallsPerNineInnings(65);
            Assert.AreEqual(RetVal, (65 / 9.0), 1E-10); 
        }
    }

    [TestClass]
    public class StrikeoutsPerNineInningsStatsTest
    {
        // Strikeouts Per Nine Innings = K / 9 innings
        [TestMethod]
        public void StrikeoutsPerNineInningsValid()
        {
            double RetVal = PitchingStats.StrikeoutsPerNineInnings(9);
            Assert.AreEqual(RetVal, (9 / 9.0) , 1E-10);
        }

        [TestMethod]
        public void StrikeoutsPerNineInningsValidLarge()  //using randy johnson 1995
        {
            double RetVal = PitchingStats.StrikeoutsPerNineInnings(294);
            Assert.AreEqual(RetVal, (294 / 9.0), 1E-10); 
        }
    }

    [TestClass]
    public class HitsPerNineInningsStatsTest
    {
        // Hits Per Nine Innings = Hits/9 innings
        [TestMethod]
        public void HitsPerNineInningsValid()
        {
            double RetVal = PitchingStats.HitsPerNineInnings(9);
            Assert.AreEqual(RetVal, (9.0 / 9), 1E-10); 
        }

        [TestMethod]
        public void HitsPerNineInningsValidLarge() //using randy johnson 1995
        {
            double RetVal = PitchingStats.HitsPerNineInnings(159);
            Assert.AreEqual(RetVal, (159 / 9.0), 1E-10); 
        }
    }

    [TestClass]
    public class InningsPitchedPerGamesStartedStatsTest
    {
        // Innings Pitched Per Game Started = Innings/Games Started
        [TestMethod]
        public void InningsPitchedPerGamesStartedValid()
        {
            double RetVal = PitchingStats.InningsPitchedPerGamesStarted(1, 1);
            Assert.AreEqual(RetVal, (1.0 / 1), 1E-10);
        }

        [TestMethod]
        public void InningsPitchedPerGamesStartedValidLarge() //using randy johnson 1995
        {
            double RetVal = PitchingStats.InningsPitchedPerGamesStarted(30, 214.1);
            Assert.AreEqual(RetVal, (214.1 / 30), 1E-10); 
        }
    }

    [TestClass]
    public class OpponentBattingAverageStatsTest
    {
        // Opponent Batting Average = hits allowed/ atbats faced
        [TestMethod]
        public void OpponentBattingAverageDivideByZero()
        {
            double RetVal = PitchingStats.OpponentsBattingAverage(1, 0);
            Assert.AreEqual(RetVal, 0); //Function Returns 0.0 for Divide by Zero
        }

        [TestMethod]
        public void OpponentBattingAverageValid()
        {
            double RetVal = PitchingStats.OpponentsBattingAverage(1, 1);
            Assert.AreEqual(RetVal, (1.0 / 1), 1E-10);
        }

        [TestMethod]
        public void OpponentBattingAverageValidLarge()
        {
            double RetVal = PitchingStats.OpponentsBattingAverage(120, 670);
            Assert.AreEqual(RetVal, (120 / 670.0), 1E-10);
        }
    }

    [TestClass]
    public class RunAverageStatsTest
    {
        //Run Average = runs scored * 9 / innings pitched
        [TestMethod]
        public void RunAverageDivideByZero()
        {
            double RetVal = PitchingStats.RunAverage(1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void RunAverageValid()
        {
            double RetVal = PitchingStats.RunAverage(1, 9);
            Assert.AreEqual(RetVal, ((1 * 9.0) / 9), 1E-10);
        }

        [TestMethod]
        public void RunAverageValidLarge()
        {
            double RetVal = PitchingStats.RunAverage(200, 1700);
            Assert.AreEqual(RetVal, ((200 * 9.0) / 1700), 1E-10);
        }
    }

    [TestClass]
    public class WalksPlusHitsPerInningsPitchedStatsTest
    {
        //Walks Plus Hits Per Inning Pitched = (H+BB)/IP
        [TestMethod]
        public void WalksPlusHitsPerInningsPitchedDivideByZero()
        {
            double RetVal = PitchingStats.WalksPlusHitsPerInningPitched(1, 1, 0);
            Assert.AreEqual(RetVal, 0);
        }

        [TestMethod]
        public void WalksPlusHitsPerInningsPitchedValid()
        {
            double RetVal = PitchingStats.WalksPlusHitsPerInningPitched(1, 1, 1);
            Assert.AreEqual(RetVal, ((1 + 1.0) / 1), 1E-10);
        }

        [TestMethod]
        public void WalksPlusHitsPerInningsPitchedValidLarge()
        {
            double RetVal = PitchingStats.WalksPlusHitsPerInningPitched(100, 50, 300);
            Assert.AreEqual(RetVal, ((100 + 50.0) / 300), 1E-10);
        }

    }
}

