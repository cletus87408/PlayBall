using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{

    public class BattingAverageOnBallsInPlay : LahmanStatsBase
    {
        public override string Name => "Batting Average on Balls in Play";

        public override string ShortName => "BABIP";

        public override string Explanation => @"Percentage of balls put in play that result in a hit.";

        public BattingAverageOnBallsInPlay(LahmanEntities db) : base(db)
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
                        thisStat.Value = AdvancedStats.BattingAverageOnBallsInPlay(hits: row.H.Value, homeRuns: row.HR.Value, atBats: row.AB.Value, strikeOuts: row.SO.Value, sacFlies: row.SF.Value);
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
                        int cumulativeH = 0, cumulativeHR = 0, cumulativeAB = 0, cumulativeSO = 0, cumulativeSF = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeSF += row.SF.Value;
                                cumulativeHR += row.HR.Value;
                                cumulativeSO += row.SO.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = AdvancedStats.BattingAverageOnBallsInPlay(hits: cumulativeH, homeRuns: cumulativeHR, atBats: cumulativeAB, strikeOuts: cumulativeSO, sacFlies: cumulativeSF);
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
                        thisStat.AddMetadataItem("Strikeout", cumulativeSO.ToString());

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
                        int cumulativeH = 0, cumulativeHR = 0, cumulativeAB = 0, cumulativeSO = 0, cumulativeSF = 0;

                        thisSeason.ToList().ForEach(
                            row =>
                            {
                                cumulativeH += row.H.Value;
                                cumulativeAB += row.AB.Value;
                                cumulativeSF += row.SF.Value;
                                cumulativeHR += row.HR.Value;
                                cumulativeSO += row.SO.Value;
                            });

                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.League };
                        thisStat.Value = AdvancedStats.BattingAverageOnBallsInPlay(hits: cumulativeH, homeRuns: cumulativeHR, atBats: cumulativeAB, strikeOuts: cumulativeSO, sacFlies: cumulativeSF);
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("SacFly", cumulativeSF.ToString());
                        thisStat.AddMetadataItem("HomeRuns", cumulativeHR.ToString());
                        thisStat.AddMetadataItem("Strikeout", cumulativeSO.ToString());

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
