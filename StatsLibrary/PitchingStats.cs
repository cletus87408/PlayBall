using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class PitchingStats
    {
        //converter for innings, takes the decimal value and multiplies it by 1/3 and returns the correct innings value
        public static double Innings(double inningsPitched)
        {
            if (inningsPitched == 0)
                return 0.0;

            var dec = (inningsPitched % 1) * (1/3);
            var realInningsPitched = inningsPitched - (inningsPitched % 1) + dec;
            return realInningsPitched;
        }
        // ERA
        // Earned Run Average = Earned Runs * 9 / Innings
        public static double EarnedRunAverage(int runs, double inningsPitched) 
        {
            return inningsPitched != 0 ? ((9.0 * runs) / Innings(inningsPitched)) : 0.0;
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
            return gamesStarted != 0 ? (Innings(inningsPitched) / gamesStarted) : 0.0;
        }

        // OBA
        // Opponent Batting Average = hits allowed/ atbats faced
        public static double OpponentsBattingAverage(int hitsAllowed, int atBatsFaced) 
        {
            return atBatsFaced != 0 ? (hitsAllowed / (1.0 * atBatsFaced)) : 0.0;
        }

        //RA
        //Run Average = runs scored * 9 / innings pitched
        public static double RunAverage(int runs, double inningsPitched)
        {
            return inningsPitched != 0 ? ((runs * 9.0) / Innings(inningsPitched)) : 0.0;
        }

        //WHIP
        //Walks Plus Hits Per Inning Pitched = (H+BB)/IP
        public static double WalksPlusHitsPerInningPitched(int hits, int walks, double inningsPitched)
        {
            return inningsPitched != 0 ? ((hits + walks) / (1.0 * Innings(inningsPitched))) : 0.0;
        }
    }
}
