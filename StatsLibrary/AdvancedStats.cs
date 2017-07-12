using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class AdvancedStats
    {
        public static double BattingAverageOnBallsInPlay(int hits, int homeRuns, int atBats, int strikeOuts, int sacFlies) //BABIP
        {
            int denominator = (atBats - strikeOuts - homeRuns + sacFlies);
            return denominator != 0 ? (hits - homeRuns) / (denominator * 1.0) : 0.0;
        }

        public static double RunsCreated(int singles, int doubles, int triples, int homeRuns, int walks, int atBats, int hits) //RC
        {
            double totalBases = BasicStats.TotalBases(singles, doubles, triples, homeRuns);
            int denominator = (atBats + walks);

            return denominator != 0 ? ((hits + walks) * totalBases) / (1.0 * denominator) : 0.0;
        }

        public static double ExtrapolatedRuns(int singles, int doubles, int triples, int homeRuns, int hitByPitch, int walks, int intentionalWalks,int stolenBases, int caughtStealing, int atBats, int hits, int strikeouts, int groundIntoDoublePlay, int sacFly, int sacHit) // XR
        {
            double XR = (.50 * singles) + (.72 * doubles) + (1.04 * triples) + (1.44 * homeRuns) + (.34 * (hitByPitch + walks - intentionalWalks)) + (.25 * intentionalWalks) + (.18 * stolenBases) + (-.32 * caughtStealing) + (-.090 * (atBats - hits - strikeouts)) + (-.098 * strikeouts) + (-.37 * groundIntoDoublePlay) + (.37 * sacFly) + (.04 * sacHit);

            return XR;
        }

        public static double EquivalentAverage(int hits, int singles, int doubles, int triples, int homeRuns, int walks, int hitByPitch, int stolenBases, int sacHit, int sacFly, int atBats, int caughtStealing) //EqA
        {
            double totalBases = BasicStats.TotalBases(singles, doubles, triples, homeRuns);
            double denominator = (atBats + walks + hitByPitch + sacHit + sacFly + caughtStealing + (stolenBases / 3.0));
            double EqA = ((hits + totalBases + (1.5 * (walks + hitByPitch)) + stolenBases + sacHit + sacFly) / denominator);

            return denominator != 0 ? EqA : 0.0;
        }
    }
}
