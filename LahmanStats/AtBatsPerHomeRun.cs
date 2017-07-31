using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    public class AtBatsPerHomeRun : LahmanStatsBase
    {
        public override string Name => "At Bats Per HomeRun";

        public override string ShortName => "AB/HR";

        public override string Explanation => @"The total number of At Bats divided by total number of homeruns";

        public AtBatsPerHomeRun(LahmanEntities db) : base(db)
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

                        thisStat.Value = BasicStats.AtBatsPerHomeRun(atBats: row.AB.Value, homeRuns: row.HR.Value);

                        yield return thisStat;
                    }
                }
            }

        }

        private IEnumerable<IStatsAck> ComputeForTeam(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            foreach (string id in identifiers)
            {
                string team = id;

                foreach (var year in Enumerable.Range(start.Year, (stop.Year - start.Year) + 1))
                {
                    var thisSeason = this.database.Battings.Team(team).DateRange((short)year, (short)year);
                    int y = year;

                    if (thisSeason.Any())
                    {
                        int cumulativeAB = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeAB += row.AB.Value;
                                cumulativeHR += row.HR.Value;
                            });

                        //construct the return object, use cumulativeRODI as placeholder
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.AtBatsPerHomeRun(atBats: cumulativeAB, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
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
                    //find matching rows
                    var thisSeason = this.database.Battings.League(league).DateRange((short)year, (short)year);
                    int y = year;

                    //if any found sum the target values
                    if (thisSeason.Any())
                    {
                        int cumulativeAB = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeAB += row.AB.Value;
                                cumulativeHR += row.HR.Value;
                            });

                        //construct the return object, use cumulativeRODI as placeholder
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.AtBatsPerHomeRun(atBats: cumulativeAB, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
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