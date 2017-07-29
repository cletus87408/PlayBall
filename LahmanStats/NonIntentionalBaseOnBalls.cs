﻿using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    // Singles plugin, Implements the IStatsPlugin interface for the
    // Batting Average statistic using the Lahman database Entity Framework objects as the data source.
    public class NonIntentionalBaseOnBalls : LahmanStatsBase
    {
        public override string Name => "Non Intentional Base On Balls";

        public override string ShortName => "NIBB";

        public override string Explanation => @"Total walks minus intentional walks.";

        public NonIntentionalBaseOnBalls(LahmanEntities db) : base(db)
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
                        thisStat.Value = BasicStats.NonIntentionalBaseOnBalls(walks: row.BB.Value, intentionalWalks: row.IBB.Value);
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
                        int cumulativeBB = 0, cumulativeIBB = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeBB += row.BB.Value;
                                cumulativeIBB += row.IBB.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.NonIntentionalBaseOnBalls(walks: cumulativeBB, intentionalWalks: cumulativeIBB);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Intentional Walks", cumulativeIBB.ToString());

                        yield return thisStat;
                    }
                    
                }
            }
        }

        private IEnumerable<IStatsAck> ComputeForLeague(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            foreach (string id in identifiers)
            {
                string league = id;

                foreach (var year in Enumerable.Range(start.Year, (stop.Year - start.Year) + 1))
                {
                    var thisSeason = this.database.Battings.League(league).DateRange((short)year, (short)year);
                    int y = year;

                    if (thisSeason.Any())
                    {
                        int cumulativeBB = 0, cumulativeIBB = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeBB += row.BB.Value;
                                cumulativeIBB += row.IBB.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.NonIntentionalBaseOnBalls(walks: cumulativeBB, intentionalWalks: cumulativeIBB);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Intentional Walks", cumulativeIBB.ToString());

                        yield return thisStat;
                    }

                }
            }
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