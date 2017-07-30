using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class BasicStats
    {
        // Batting

        // 1B
        // Singles = H - 2B - 3B - HR
        public static double Singles(int hits, int doubles, int triples, int homeRuns)
        {
            return (hits - doubles - triples - homeRuns);
        }

        // PA
        // PA = AB + BB + HBP + SH + SF + Times Reached on Defensive Interference
        public static double PlateAppearances(int atBats, int walks, int hitByPitch, int sacHit, int sacFly, int reachedOnDefensiveInterference)
        {
            return (atBats + walks + hitByPitch + sacHit + sacFly + reachedOnDefensiveInterference);
        }

        //NIBB
        //Non Intentional Base on Balls = BB - IBB
        public static double NonIntentionalBaseOnBalls(int walks, int intentionalWalks)
        {
            return (walks - (1.0 * intentionalWalks));
        }

        // BA
        // Batting Average = H / AB
        public static double BattingAverage(int atBats, int hits)
        {
            return atBats != 0 ? hits / (1.0 * atBats) : 0.0;
        }

        // AB/HR
        // At Bats Per Homerun = AB / HR
        public static double AtBatsPerHomeRun(int atBats, int homeRuns) 
        {
            return homeRuns != 0 ? atBats / (1.0 * homeRuns) : 0.0;
        }

        // BB/K
        // Walks to Strikeouts = BB / K
        public static double WalkToStrikeoutRatio(int walks, int strikeouts) 
        {
            return strikeouts != 0 ? walks / (1.0 * strikeouts) : 0.0; 
        }

        // PA/SO
        // Plate Appearances Per Strikeout = PA / K
        // PA = AB + BB + HBP + SH + SF + Times Reached on Defensive Interference
        public static double PlateAppearancesPerStrikeout(int atBats, int walks, int hitByPitch, int sacHit, int sacFly, int reachedOnDefensiveInterference, int strikeouts) 
        {
            double plateAppearances = BasicStats.PlateAppearances(atBats, walks, hitByPitch, sacHit, sacFly, reachedOnDefensiveInterference);

            return strikeouts != 0 ?  (plateAppearances / (1.0 * strikeouts)) : 0.0;
        }

        // TB
        // Total Bases = (1B + (2B * 2) + (3B *3) + (HR * 4))
        public static double TotalBases(int hits, int doubles, int triples, int homeRuns) 
        {
            double singles = BasicStats.Singles(hits, doubles, triples, homeRuns);
            return (1.0 * singles + 2.0 * doubles + 3.0 * triples + 4.0 * homeRuns);
        }

        // RP
        // Runs Produced = R + RBI - HR
        public static double RunsProduced(int runs, int runsBattedIn, int homeRuns) 
        {
            return (1.0 * runs + runsBattedIn - homeRuns);
        }

        // SLG
        // Slugging Percentage = ((1B + (2B * 2) + (3B * 3) + (4B * 4))/AB)
        public static double SluggingPercentage(int hits, int doubles, int triples, int homeRuns, int atBats) 
        {
            double totalBases = BasicStats.TotalBases(hits, doubles, triples, homeRuns);

            return atBats != 0 ? totalBases / (1.0 * atBats) : 0.0;
        }

        // TA
        // Total Average = ((TB + BB + HBP + SB) / (AB - H + CS + GIDP))
        public static double TotalAverage(int doubles, int triples, int homeRuns, int hitByPitch, int walks, int stolenBases, int atBats, int hits, int caughtStealing, int groundedIntoDoublePlay) 
        {
            double totalBases = BasicStats.TotalBases(hits, doubles, triples, homeRuns);
            int denominator = (atBats - hits + caughtStealing + groundedIntoDoublePlay);

            return denominator != 0 ? (totalBases + hitByPitch + walks + stolenBases) / (1.0 * denominator) : 0.0;
        }

        // TOB
        // Times On Base = H + BB + HBP
        public static double TimesOnBase(int hits, int walks, int hitByPitch) 
        {
            return (1.0 * hits + walks + hitByPitch);
        }

        // XBH
        // Extra Base Hits = (2B + 3B + HR)
        public static double ExtraBaseHits(int doubles, int triples, int homeRuns) 
        {
            return (1.0 * doubles + triples + homeRuns);
        }

        // OBP
        // On Base Percentage = (H + BB + HBP)/(AB + BB + HBP + SF)
        public static double OnBasePercentage(int hits, int walks, int hitByPitch, int atBats, int sacFly)
        {
            int denominator = (atBats + walks + hitByPitch + sacFly);

            return denominator != 0 ? (hits + walks + hitByPitch) / (1.0 * denominator) : 0.0;
        }

        // OPS
        // On Base Plus Slugging = Slugging Percentage + On Base Percentage
        // ((1B + (2B * 2) + (3B * 3) + (4B * 4))/AB) + ((H + BB + HBP)/(AB + BB + HBP + SF))  
        public static double OnBasePlusSlugging(int hits, int walks, int hitByPitch, int atBats, int sacFly, int doubles, int triples, int homeRuns) 
        {
            double slugging = BasicStats.SluggingPercentage(hits, doubles, triples, homeRuns, atBats);
            double onBase = BasicStats.OnBasePercentage(hits, walks, hitByPitch, atBats, sacFly);

            return (slugging + onBase);
        }

        // BaseRunning

        // SB%
        // Stolen Base Percentage = Stolen Bases/ Attempts
        public static double StolenBasePercentage(int stolenBases, int caughtStealing) 
        {
            int stolenBasesAttempted = (stolenBases + caughtStealing);
            return stolenBasesAttempted != 0 ? (stolenBases / (1.0 * stolenBasesAttempted)) : 0.0;
        }


        // Fielding

        // TC
        // Total Chances = Assists + Putouts + Errors 
        public static double TotalChances(int assists, int putouts, int errors) 
        {
            return (assists + putouts + errors);
        }

        // FP
        // Fielding Percentage = (Chances - Errors) / Total Chances
        public static double FieldingPercentage(int assists, int putouts, int errors) 
        {
            double chances = BasicStats.TotalChances(assists, putouts, errors);
            
            return chances != 0 ? ((chances - errors) / (1.0 * chances)) : 0.0;
        }

        // RF
        // Range Factor = 9 * (putouts + assists) / innings played
        public static double RangeFactor(int putouts, int assists, double inningsPlayed) 
        {
            return inningsPlayed != 0 ? ((9.0 * (putouts + assists)) / inningsPlayed) : 0.0;
        }
    }
}
