using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLibrary
{
    public static class BasicStats
    {
        public static double BattingAverage(int atBats, int hits)
        {
            return atBats != 0 ? hits / (1.0 * atBats) : 0.0;
        }
    }

    public static class AdvancedStats
    {
        public static double OPS()
        {
            return 1;
        }
    }


}
