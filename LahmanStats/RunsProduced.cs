using System;
using System.Collections.Generic;
using System.Linq;
using LahmanDatabaseEntities;
using StatsLibrary;
using StatsManager;

namespace LahmanStats
{
    public class RunsProduced : LahmanStatsBase
    {
        public override string Name => "Runs Produced";

        public override string ShortName => "RP";

        public override string Explanation => @"Runs plus runs batted in minus homeruns.";

        public RunsProduced(LahmanEntities db) : base(db)
        {

        }


        private IEnumerable<IStatsAck> ComputeForIndividual(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
     
            foreach (string id in identifiers)
            {
                var matchingRows = this.database.Battings.Individual(id).DateRange((short)start.Year, (short)stop.Year);

                if (matchingRows.Any())
                {
                    foreach (var row in matchingRows)
                    {
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(row.yearID, 1, 1), Stop = new DateTime(row.yearID, 1, 1), Target = StatsTarget.Individual };

                        thisStat.Value = BasicStats.RunsProduced(runs: row.R.Value, runsBattedIn: row.RBI.Value, homeRuns: row.HR.Value);

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
                        int cumulativeR = 0, cumulativeRBI = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeR += row.R.Value;
                                cumulativeRBI += row.RBI.Value;
                                cumulativeHR += row.HR.Value;
                            });

                  
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.RunsProduced(runs: cumulativeR, runsBattedIn: cumulativeRBI, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Runs", cumulativeR.ToString());
                        thisStat.AddMetadataItem("RunsBattedIn", cumulativeRBI.ToString());
                        thisStat.AddMetadataItem("Homeruns", cumulativeHR.ToString());
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
                        int cumulativeR = 0, cumulativeRBI = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeR += row.R.Value;
                                cumulativeRBI += row.RBI.Value;
                                cumulativeHR += row.HR.Value;
                            });

   
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.RunsProduced(runs: cumulativeR, runsBattedIn: cumulativeRBI, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Runs", cumulativeR.ToString());
                        thisStat.AddMetadataItem("RunsBattedIn", cumulativeRBI.ToString());
                        thisStat.AddMetadataItem("Homeruns", cumulativeHR.ToString());
                        yield return thisStat;
                    }
                }
            }
        }

     
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