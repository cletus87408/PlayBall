﻿using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    public class TimesOnBase : LahmanStatsBase
    {
        public override string Name => "Times on Base";

        public override string ShortName => "TOB";

        public override string Explanation => @"Hits plus walks plus HBP";

        public TimesOnBase(LahmanEntities db) : base(db)
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

                        thisStat.Value = BasicStats.TimesOnBase(hits: row.H.Value, walks: row.BB.Value, hitByPitch: row.HBP.Value);

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
                        int cumulativeH = 0, cumulativeBB = 0, cumulativeHBP = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulativeBB += row.BB.Value;
                                cumulativeHBP += row.HBP.Value;
                            });

                  
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.TimesOnBase(hits: cumulativeH, walks: cumulativeBB, hitByPitch: cumulativeHBP);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
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
                        int cumulativeH = 0, cumulativeBB = 0, cumulativeHBP = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulativeBB += row.BB.Value;
                                cumulativeHBP += row.HBP.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.TimesOnBase(hits: cumulativeH, walks: cumulativeBB, hitByPitch: cumulativeHBP);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
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