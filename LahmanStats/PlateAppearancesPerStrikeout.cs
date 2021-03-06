﻿using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using StatsLibrary;
using StatsManager;

namespace LahmanStats
{
    public class PlateAppearancesPerStrikeout : LahmanStatsBase
    {
        public override string Name => "At Bats Per HomeRun";

        public override string ShortName => "AB/HR";

        public override string Explanation => @"The total number of At Bats divided by total number of homeruns";

        public PlateAppearancesPerStrikeout(LahmanEntities db) : base(db)
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

                        //had to use placeholder variable since we have no data for reached on defensive interference
                        int reachedOnDefensiveInterference = 0; //placeholder

                        thisStat.Value = BasicStats.PlateAppearancesPerStrikeout(atBats: row.AB.Value, walks: row.BB.Value, hitByPitch: row.HBP.Value, sacHit: row.SH.Value, sacFly: row.SF.Value, reachedOnDefensiveInterference: reachedOnDefensiveInterference, strikeouts: row.SO.Value);

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
                        int cumulativeBB = 0, cumulativeSO = 0, cumulativeAB = 0, cumulativeHBP = 0, cumulativeSH = 0, cumulativeSF = 0, cumulativeRODI = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeBB += row.BB.Value;
                                cumulativeSO += row.SO.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeHBP += row.HBP.Value;
                                cumulativeSF += row.SF.Value;
                                cumulativeSH += row.SH.Value;
                            });

                        //construct the return object, use cumulativeRODI as placeholder
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.PlateAppearancesPerStrikeout(atBats: cumulativeAB, walks: cumulativeBB, hitByPitch: cumulativeHBP, sacHit: cumulativeSH, sacFly: cumulativeSF, reachedOnDefensiveInterference: cumulativeRODI, strikeouts: cumulativeSO);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Strikeouts", cumulativeSO.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
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
                        int cumulativeBB = 0, cumulativeSO = 0, cumulativeAB = 0, cumulativeHBP = 0, cumulativeSH = 0, cumulativeSF = 0, cumulativeRODI = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeBB += row.BB.Value;
                                cumulativeSO += row.SO.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeHBP += row.HBP.Value;
                                cumulativeSF += row.SF.Value;
                                cumulativeSH += row.SH.Value;
                            });

                        //construct the return object, use cumulativeRODI as placeholder
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = BasicStats.PlateAppearancesPerStrikeout(atBats: cumulativeAB, walks: cumulativeBB, hitByPitch: cumulativeHBP, sacHit: cumulativeSH, sacFly: cumulativeSF, reachedOnDefensiveInterference: cumulativeRODI, strikeouts: cumulativeSO);
                        thisStat.AddMetadataItem("Walks", cumulativeBB.ToString());
                        thisStat.AddMetadataItem("Strikeouts", cumulativeSO.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("HitByPitch", cumulativeHBP.ToString());
                        thisStat.AddMetadataItem("SacHit", cumulativeSH.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());
                        thisStat.AddMetadataItem("ReachedOnDefensiveInterference", cumulativeRODI.ToString());
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