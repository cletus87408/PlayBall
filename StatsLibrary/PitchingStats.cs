using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class PitchingStats
    {
        public static double EarnedRunAverage(int runs, double inningsPitched) // ERA
        {
            return inningsPitched != 0 ? ((9.0 * runs) / inningsPitched) : 0.0;
        }

        public static double BaseOnBallsPerNineInnings(int walks) // BB/9
        {
            return (walks / 9.0);
        }

        public static double StrikeoutsPerNineInnings(int strikeouts) // K/9
        {
            return (strikeouts / 9.0);
        }

        public static double HitsPerNineInnings(int hits) // H/9
        {
            return (hits / 9.0);
        }

        public static double InningsPitchedPerGamesStarted(int gamesStarted, double inningsPitched) // IP/GS
        {
            return (inningsPitched / gamesStarted);
        }

        public static double OpponentsBattingAverage(int hitsAllowed, int atBatsFaced) // OBA
        {
            return atBatsFaced != 0 ? (hitsAllowed / (1.0 * atBatsFaced)) : 0.0;
        }
    }
}
