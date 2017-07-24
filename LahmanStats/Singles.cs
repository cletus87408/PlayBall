using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    // Singles plugin, Implements the IStatsPlugin interface for the
    // Batting Average statistic using the Lahman database Entity Framework objects as the data source.
    public class Singles : LahmanStatsBase
    {
        public override string Name => "Singles";

        public override string ShortName => "1B";

        public override string Explanation => @"Hits that advanced the batter only to first base";

        public Singles(LahmanEntities db) : base(db)
        {

        }


        private IEnumerable<IStatsAck> ComputeForIndividual(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            // For every player in the list...
            foreach (string id in identifiers)
            {
                // Find the players entries in the batting database
                var playerRows = from row in this.database.Battings where row.playerID == id select row;
                // Find those rows from the players entries that match the years requested 
                // Note that for the Lahman database, time granularity is by year
                var matchingRows = from row in playerRows
                                   where row.yearID >= start.Year
                                   where row.yearID <= stop.Year
                                   select row;

                if (matchingRows.Any())
                {
                    foreach (var row in matchingRows)
                    {
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(row.yearID, 1, 1), Stop = new DateTime(row.yearID, 1, 1), Target = StatsTarget.Individual };
                        thisStat.Value = BasicStats.Singles(hits: row.H.Value, doubles: row.C2B.Value, triples: row.C3B.Value, homeRuns: row.HR.Value);
                        yield return thisStat;
                    }
                }
            }

        }

        private IEnumerable<IStatsAck> ComputeForTeam(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            // TODO: Not yet implemented.  Will work on later
            return null;
        }

        private IEnumerable<IStatsAck> ComputeForLeague(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            // TODO: Not yet implemented.  Will work on later 
            return null;
        }

        // Computes the Batting Average stat for each identifier in the incoming list (might be more than
        // one player/team/league per invocation).
        public override IEnumerable<IStatsAck> Compute(IEnumerable<string> identifiers, StatsTarget target, DateTime start, DateTime stop)
        {
            switch (target)
            {
                case StatsTarget.Individual: return ComputeForIndividual(identifiers, start, stop);
                case StatsTarget.Team: return ComputeForTeam(identifiers, start, stop);
                case StatsTarget.League: return ComputeForLeague(identifiers, start, stop);
                default: return null;
            }
        }
    }
}
