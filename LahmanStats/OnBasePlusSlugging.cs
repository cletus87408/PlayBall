using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using StatsLibrary;
using StatsManager;

namespace LahmanStats
{
    public class OnBasePlusSlugging : LahmanStatsBase
    {
        public override string Name => "On Base Plus Slugging Percentage";

        public override string ShortName => "OPS";

        public override string Explanation => @"On Base Percentage + Slugging Percentage. ";

        public OnBasePlusSlugging(LahmanEntities db) : base(db)
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

                        thisStat.Value = BasicStats.OnBasePlusSlugging(hits: row.H.Value, walks: row.BB.Value, hitByPitch: row.HBP.Value, atBats: row.AB.Value, sacFly: row.SF.Value, doubles: row.C2B.Value, triples: row.C3B.Value, homeRuns: row.HR.Value);

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
                        int cumulativeH = 0, cumulative2B = 0, cumulative3B = 0, cumulativeHR = 0, cumulativeAB = 0, cumulativeBB = 0, cumulativeHBP = 0, cumulativeSF = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulative2B += row.C2B.Value;
                                cumulative3B += row.C3B.Value;
                                cumulativeHR += row.HR.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeBB += row.BB.Value;
                                cumulativeHBP += row.HBP.Value;
                                cumulativeSF += row.SF.Value;
                            });

                  
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.OnBasePlusSlugging(hits: cumulativeH, walks: cumulativeBB, hitByPitch: cumulativeHBP, atBats: cumulativeAB, sacFly: cumulativeSF, doubles: cumulative2B, triples: cumulative3B, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("Doubles", cumulative2B.ToString());
                        thisStat.AddMetadataItem("Triples", cumulative3B.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());

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
                        int cumulativeH = 0, cumulative2B = 0, cumulative3B = 0, cumulativeHR = 0, cumulativeAB = 0, cumulativeBB = 0, cumulativeHBP = 0, cumulativeSF = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulative2B += row.C2B.Value;
                                cumulative3B += row.C3B.Value;
                                cumulativeHR += row.HR.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeBB += row.BB.Value;
                                cumulativeHBP += row.HBP.Value;
                                cumulativeSF += row.SF.Value;
                            });


                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.OnBasePlusSlugging(hits: cumulativeH, walks: cumulativeBB, hitByPitch: cumulativeHBP, atBats: cumulativeAB, sacFly: cumulativeSF, doubles: cumulative2B, triples: cumulative3B, homeRuns: cumulativeHR);
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("Doubles", cumulative2B.ToString());
                        thisStat.AddMetadataItem("Triples", cumulative3B.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());

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