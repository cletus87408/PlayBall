using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    public class TotalChances : LahmanStatsBase
    {
        public override string Name => "Total Chances";

        public override string ShortName => "TC";

        public override string Explanation => @"Total chances to make a defensive play: Assists + Putouts + Errors.";

        public TotalChances(LahmanEntities db) : base(db)
        {

        }


        private IEnumerable<IStatsAck> ComputeForIndividual(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
     
            foreach (string id in identifiers)
            {
                var matchingRows = this.database.Fieldings.Individual(id).DateRange((short)start.Year, (short)stop.Year);

                if (matchingRows.Any())
                {
                    foreach (var row in matchingRows)
                    {
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(row.yearID, 1, 1), Stop = new DateTime(row.yearID, 1, 1), Target = StatsTarget.Individual };

                        thisStat.Value = BasicStats.TotalChances(assists: row.A.Value, putouts: row.PO.Value, errors: row.E.Value);

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
                    int y = year;
                    var thisSeason = this.database.Fieldings.Team(team).DateRange((short)year, (short)year);

                    if (thisSeason.Any())
                    {
                        int cumulativeA = 0, cumulativePO = 0, cumulativeE = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeA += row.A.Value;
                                cumulativePO += row.PO.Value;
                                cumulativeE += row.E.Value;
                            });

                  
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.TotalChances(assists: cumulativeA, putouts: cumulativePO, errors: cumulativeE);
                        thisStat.AddMetadataItem("Assists", cumulativeA.ToString());
                        thisStat.AddMetadataItem("PutOuts", cumulativePO.ToString());
                        thisStat.AddMetadataItem("Errors", cumulativeE.ToString());
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
                    int y = year;
                    var thisSeason = this.database.Fieldings.League(league).DateRange((short)year, (short)year);

                    //if any found sum the target values
                    if (thisSeason.Any())
                    {
                        int cumulativeA = 0, cumulativePO = 0, cumulativeE = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeA += row.A.Value;
                                cumulativePO += row.PO.Value;
                                cumulativeE += row.E.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.TotalChances(assists: cumulativeA, putouts: cumulativePO, errors: cumulativeE);
                        thisStat.AddMetadataItem("Assists", cumulativeA.ToString());
                        thisStat.AddMetadataItem("PutOuts", cumulativePO.ToString());
                        thisStat.AddMetadataItem("Errors", cumulativeE.ToString());
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