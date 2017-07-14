using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class AdvancedStats
    {
        // BABIP
        // Batting Average On Balls In Play = ((H - HR) / (AB - K - HR +SF))
        public static double BattingAverageOnBallsInPlay(int hits, int homeRuns, int atBats, int strikeOuts, int sacFlies) 
        {
            int denominator = (atBats - strikeOuts - homeRuns + sacFlies);
            return denominator != 0 ? (hits - homeRuns) / (denominator * 1.0) : 0.0;
        }

        // RC
        // Runs Created = ((H + BB) * TB)/ (AB + BB)
        public static double RunsCreated(int doubles, int triples, int homeRuns, int walks, int atBats, int hits) 
        {
            double totalBases = BasicStats.TotalBases(hits, doubles, triples, homeRuns);
            int denominator = (atBats + walks);

            return denominator != 0 ? ((hits + walks) * totalBases) / (1.0 * denominator) : 0.0;
        }

        // XR
        // Extrapolated Runs = (.50 * 1B) + (.72 * 2B) + (1.04 * 3B) + (1.44 * HR) + (.34 * (HBP+TBB-IBB)) + (.25 * IBB) + (.18 * SB) + (-.32 * CS) + (-.090 * (AB-H-K)) +(-.098 * K) + (-.37 * GIDP) + (.37 * SF) + (.04 * SH)
        public static double ExtrapolatedRuns(int doubles, int triples, int homeRuns, int hitByPitch, int walks, int intentionalWalks,int stolenBases, int caughtStealing, int atBats, int hits, int strikeouts, int groundIntoDoublePlay, int sacFly, int sacHit) 
        {
            double singles = BasicStats.Singles(hits, doubles, triples, homeRuns);

            double XR = (.50 * singles) + (.72 * doubles) + (1.04 * triples) + (1.44 * homeRuns) + (.34 * (hitByPitch + walks - intentionalWalks)) + (.25 * intentionalWalks) + (.18 * stolenBases) + (-.32 * caughtStealing) + (-.090 * (atBats - hits - strikeouts)) + (-.098 * strikeouts) + (-.37 * groundIntoDoublePlay) + (.37 * sacFly) + (.04 * sacHit);

            return XR;
        }

        // EqA
        // Equivalent Average = (Hits + Total Bases + 1.5 * (Walks + Hit By Pitch) + Stolen Bases + Sacrifice Hits + Sacrifice Flies) / (At bats + Walks + Hit By Pitch + Sac Hits + Sac Flies + Caught Stealing + (Stolen Bases / 3) )
        public static double EquivalentAverage(int hits, int doubles, int triples, int homeRuns, int walks, int hitByPitch, int stolenBases, int sacHit, int sacFly, int atBats, int caughtStealing) 
        {
            double totalBases = BasicStats.TotalBases(hits, doubles, triples, homeRuns);
            double denominator = (atBats + walks + hitByPitch + sacHit + sacFly + caughtStealing + (stolenBases / 3.0));
            double EqA = ((hits + totalBases + (1.5 * (walks + hitByPitch)) + stolenBases + sacHit + sacFly) / denominator);

            return denominator != 0 ? EqA : 0.0;
        }

        // BsR (Base Runs) = (A*B)/(B+C)+D
        // where A= H + BB - HR, B = (1.4 * TB - 0.6 * H - 3 * HR + 0.1 * BB) * 1.02, C = AB - H, D = HR
        public static double BaseRuns(int hits, int walks, int homeRuns, int doubles, int triples, int atBats)
        {
            double totalBases = BasicStats.TotalBases(hits, doubles, triples, homeRuns);
            int A = hits + walks - homeRuns;
            int C = atBats - hits;
            double B = (1.4 * totalBases - 0.6 * hits - 3 * homeRuns + 0.1 * walks) * 1.02;

            return (B + C) != 0 ? (((A * B) / (B + C)) + homeRuns) : 0.0;
        }

        // GPA
        // Gross Production Average = (1.8 * OPS) / 4
        public static double GrossProductionAverage(int doubles, int triples, int homeRuns, int atBats, int hits, int walks, int hitByPitch, int sacFly)
        {
            double OPS = BasicStats.OnBasePlusSlugging(hits, walks, hitByPitch, atBats, sacFly, doubles, triples, homeRuns);
            return ((1.8 * OPS) / 4);
        }
    }
}
