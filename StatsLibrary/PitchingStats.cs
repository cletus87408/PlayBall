using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class PitchingStats
    {
        // ERA
        // Earned Run Average = Earned Runs * 9 / Innings
        public static double EarnedRunAverage(int runs, double inningsPitched) 
        {
            return inningsPitched != 0 ? ((9.0 * runs) / inningsPitched) : 0.0;
        }

        // BB/9
        // Base On Balls Per Nine Innings = BB / 9 innings
        public static double BaseOnBallsPerNineInnings(int walks) 
        {
            return (walks / 9.0);
        }

        // K/9
        // Strikeouts Per Nine Innings = K / 9 innings
        public static double StrikeoutsPerNineInnings(int strikeouts) 
        {
            return (strikeouts / 9.0);
        }

        // H/9
        // Hits Per Nine Innings = Hits/9 innings
        public static double HitsPerNineInnings(int hits) 
        {
            return (hits / 9.0);
        }

        // IP/GS
        // Innings Pitched Per Game Started = Innings/Games Started
        public static double InningsPitchedPerGamesStarted(int gamesStarted, double inningsPitched) 
        {
            return (inningsPitched / gamesStarted);
        }

        // OBA
        // Opponent Batting Average = hits allowed/ atbats faced
        public static double OpponentsBattingAverage(int hitsAllowed, int atBatsFaced) 
        {
            return atBatsFaced != 0 ? (hitsAllowed / (1.0 * atBatsFaced)) : 0.0;
        }

        //RA
        //Run Average = runs scored * 9 / innings pitched
        public static double RunAverage(int runs, int inningsPitched)
        {
            return inningsPitched != 0 ? ((runs * 9.0) / inningsPitched) : 0.0;
        }

        //WHIP
        //Walks Plus Hits Per Inning Pitched = (H+BB)/IP
        public static double WalksPlusHitsPerInningPitched(int hits, int walks, int inningsPitched)
        {
            return inningsPitched != 0 ? ((hits + walks) / (1.0 * inningsPitched)) : 0.0;
        }
    }
}
