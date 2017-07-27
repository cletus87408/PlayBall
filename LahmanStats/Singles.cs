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
                var matchingRows = this.database.Battings.Individual(id).DateRange((short)start.Year, (short)stop.Year);

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
            foreach(string id in identifiers)
            {
                string team = id;

                foreach (var year in Enumerable.Range(start.Year, (stop.Year - start.Year) + 1))
                {
                    var thisSeason = this.database.Battings.Team(team).DateRange((short)year, (short)year);
                    int y = year;

                    if(thisSeason.Any())
                    {
                        int cumulativeH = 0, cumulative2B = 0, cumulative3B = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulative2B += row.C2B.Value;
                                cumulative3B += row.C3B.Value;
                                cumulativeHR += row.HR.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.Singles(hits: cumulativeH, doubles: cumulative2B, triples: cumulative3B, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("Doubles", cumulative2B.ToString());
                        thisStat.AddMetadataItem("Triples", cumulative3B.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());

                        yield return thisStat;
                    }
                    
                }
            }
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
