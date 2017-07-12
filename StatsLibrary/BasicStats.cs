using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class BasicStats
    {
        //Batting
        public static double BattingAverage(int atBats, int hits) // BA
        {
            return atBats != 0 ? hits / (1.0 * atBats) : 0.0;
        }

        public static double AtBatsPerHomeRun(int atBats, int homeRuns) // AB/HR
        {
            return homeRuns != 0 ? atBats / (1.0 * homeRuns) : 0.0;
        }

        public static double WalkToStrikeoutRatio(int walks, int strikeouts) // BB/K
        {
            return strikeouts != 0 ? walks / (1.0 * strikeouts) : 0.0; 
        }

        public static double PlateAppearancesPerStrikeout(int plateAppearances, int strikeouts) // PA/SO
        {
            return strikeouts != 0 ? plateAppearances / (1.0 * strikeouts) : 0.0;
        }

        public static double TotalBases(int singles, int doubles, int triples, int homeRuns) // TB
        {
            return (1.0 * singles + 2.0 * doubles + 3.0 * triples + 4.0 * homeRuns);
        }

        public static double RunsProduced(int runs, int runsBattedIn, int homeRuns) // RP
        {
            return (1.0 * runs + runsBattedIn - homeRuns);
        }

        public static double SluggingPercentage(int singles, int doubles, int triples, int homeRuns, int atBats) // SLG
        {
            double totalBases = BasicStats.TotalBases(singles, doubles, triples, homeRuns);

            return atBats != 0 ? totalBases / (1.0 * atBats) : 0.0;
        }

        public static double TotalAverage(int singles, int doubles, int triples, int homeRuns, int hitByPitch, int walks, int stolenBases, int atBats, int hits, int caughtStealing, int groundedIntoDoublePlay) // TA
        {
            double totalBases = BasicStats.TotalBases(singles, doubles, triples, homeRuns);
            int denominator = (atBats - hits + caughtStealing + groundedIntoDoublePlay);

            return denominator != 0 ? (totalBases + hitByPitch + walks + stolenBases) / (1.0 * denominator) : 0.0;
        }

        public static double TimesOnBase(int hits, int walks, int hitByPitch)
        {
            return (1.0 * hits + walks + hitByPitch);
        }

        public static double ExtraBaseHits(int doubles, int triples, int homeRuns) // XBH
        {
            return (1.0 * doubles + triples + homeRuns);
        }

        //BaseRunning
        public static double StolenBasePercentage(int stolenBases, int stolenBasesAttempted) //SB%
        {
            return stolenBasesAttempted != 0 ? (stolenBases / (1.0 * stolenBasesAttempted)) : 0.0;
        }

        //Fielding
        public static double TotalChances(int assists, int putouts, int errors) //TC
        {
            return (assists + putouts + errors);
        }

        public static double FieldingPercentage(int assists, int putouts, int errors) //FP
        {
            double chances = BasicStats.TotalChances(assists, putouts, errors);
            
            return chances != 0 ? ((chances - errors) / (1.0 * chances)) : 0.0;
        }

        public static double RangeFactor(int putouts, int assists, double inningsPlayed) //RF
        {
            return inningsPlayed != 0 ? ((9.0 * (putouts + assists)) / inningsPlayed) : 0.0;
        }
    }
}
