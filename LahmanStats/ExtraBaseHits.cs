using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    public class ExtraBaseHits : LahmanStatsBase
    {
        public override string Name => "Extra Base Hits";

        public override string ShortName => "XBH";

        public override string Explanation => @"Unweighted Doubles + Triples + HomeRuns";

        public ExtraBaseHits(LahmanEntities db) : base(db)
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

                        thisStat.Value = BasicStats.ExtraBaseHits(doubles: row.C2B.Value, triples: row.C3B.Value, homeRuns: row.HR.Value);

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
                        int cumulative2B = 0, cumulative3B = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulative2B += row.C2B.Value;
                                cumulative3B += row.C3B.Value;
                                cumulativeHR += row.HR.Value;
                            });

                  
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.ExtraBaseHits(doubles: cumulative2B, triples: cumulative3B, homeRuns: cumulativeHR);
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
                        int cumulative2B = 0, cumulative3B = 0, cumulativeHR = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulative2B += row.C2B.Value;
                                cumulative3B += row.C3B.Value;
                                cumulativeHR += row.HR.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.ExtraBaseHits(doubles: cumulative2B, triples: cumulative3B, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Doubles", cumulative2B.ToString());
                        thisStat.AddMetadataItem("Triples", cumulative3B.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
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