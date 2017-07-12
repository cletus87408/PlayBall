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
    }
}
