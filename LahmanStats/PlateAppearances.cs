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
    public class PlateAppearances : LahmanStatsBase
    {
        public override string Name => "Plate Appearances";

        public override string ShortName => "PA";

        public override string Explanation => @"Total times a batter stepped up to the plate. Includes At Bats and any other form of reaching base.";

        public PlateAppearances(LahmanEntities db) : base(db)
        {

        }


        private IEnumerable<IStatsAck> ComputeForIndividual(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            // For every player in the list...
            foreach (string id in identifiers)
            {
                var matchingRows = SearchForIndividual(id, start, stop);

                if (matchingRows.Any())
                {
                    foreach (var row in matchingRows)
                    {
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(row.yearID, 1, 1), Stop = new DateTime(row.yearID, 1, 1), Target = StatsTarget.Individual };

                        // I had to create a placeholder variable since reached on defensive interference is not a value in our database (nor can it be calculated from available data)
                        int reachedOnDefensiveInterference = 0; // placeholder
                        thisStat.Value = BasicStats.PlateAppearances(atBats: row.AB.Value, walks: row.BB.Value, hitByPitch: row.HBP.Value, sacHit: row.SH.Value, sacFly: row.SF.Value, reachedOnDefensiveInterference: reachedOnDefensiveInterference); 

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
                    var thisSeason = this.SearchForTeam(team, year);
                    int y = year;

                    if(thisSeason.Any())
                    {
                        int cumulativeAB = 0, cumulativeBB = 0, cumulativeHBP = 0, cumulativeSH = 0, cumulativeSF = 0, cumulativeRODI = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeAB += row.AB.Value;
                                cumulativeBB += row.BB.Value;
                                cumulativeHBP += row.HBP.Value;
                                cumulativeSF += row.SF.Value;
                                cumulativeSH += row.SH.Value;
                            });

                        //construct the return object, use cumulativeRODI as placeholder
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.PlateAppearances(atBats: cumulativeAB, walks: cumulativeBB, hitByPitch: cumulativeHBP, sacHit: cumulativeSH, sacFly: cumulativeSF, reachedOnDefensiveInterference: cumulativeRODI);
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
                        thisStat.AddMetadataItem("SacHit", cumulativeSH.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());
                        thisStat.AddMetadataItem("ReachedOnDefensiveInterference", cumulativeRODI.ToString());

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
