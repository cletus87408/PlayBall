// ***********************************************************************
// Assembly         : LahmanStats
// Author           : Bob
// Created          : 07-19-2017
//
// Last Modified By : Bob
// Last Modified On : 07-19-2017
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using StatsLibrary;
using StatsManager;
using WpfApplication1;

namespace LahmanStats
{
    using System.Linq.Expressions;

    /// <summary>
    /// Class BattingAverage.  Implements the IStatsPlugin interface for the
    /// Batting Average statistic using the Lahman database Entity Framework objects as the data source.
    /// </summary>
    /// <seealso cref="LahmanStats.LahmanStatsBase" />
    public class BattingAverage : LahmanStatsBase
    {
        /// <summary>
        /// Gets the name of the stat.  The name should be reasonably descriptive, full language text
        /// Something like "Wins above replacement" would do nicely.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "Batting Average";

        /// <summary>
        /// Gets the short name.  This is the acronym or the short form.  Something like "WAR"
        /// </summary>
        /// <value>The short name.</value>
        public override string ShortName => "BA";

        /// <summary>
        /// Gets the explanation for a stat.  A descriptive string discussing what the stat represents.
        /// This will be used by the stats management gui to provide meaningful data for the user
        /// </summary>
        /// <value>The explanation.</value>
        public override string Explanation => @"Simple batting average - hits divided by at bats";

        /// <summary>
        /// Initializes a new instance of the <see cref="BattingAverage"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public BattingAverage(LahmanEntities db) : base(db)
        {
        }

        /// <summary>
        /// Computes batting average for individuals.
        /// </summary>
        /// <param name="identifiers">The player identifiers.</param>
        /// <param name="start">The start year.</param>
        /// <param name="stop">The stop year.</param>
        /// <returns>IEnumerable&lt;IStatsAck&gt;.</returns>
        private IEnumerable<IStatsAck> ComputeForIndividual(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            // For every player in the list...
            foreach (string id in identifiers)
            {
                var matchingRows = this.SearchForIndividual(id, start, stop);

                // Did we find any matches?
                if (matchingRows.Any())
                {
                    foreach (var row in matchingRows)
                    {
                        // Make one return value for every matching entry, using the stats library to compute the BA
                        StatsAck thisStat = new StatsAck {Identifier = id, Start = new DateTime(row.yearID, 1, 1), Stop = new DateTime(row.yearID, 12, 31) , Target = StatsTarget.Individual};
                        thisStat.Value = BasicStats.BattingAverage(atBats:row.AB.Value, hits:row.H.Value);
                        thisStat.AddMetadataItem("AtBats", row.AB.Value.ToString());
                        thisStat.AddMetadataItem("Hits", row.H.Value.ToString());
                        yield return thisStat;
                    }

                    // Exercise for the reader:  I want one more stat which is the total individual batting average over ALL of the years.  That is, if I asked for
                    // stats for 2006, 2007, and 2008, then I want one entry for each of those years (which is the code above) plus one more summary stat for 
                    // all three years.  You do notneed to create a new Linq query for the purpose - all of the data has been generated above, you just need 
                    // to add a little code to save what you need for the summary statistic.  The Start and Stop of the last summary should indicate the entire
                    // range of that summary statistic.  If there is only a single year requested, then I only want the one entry
                    // Fill in the metadata like above, with the total atbats and hits for reference
                }
            }
        }

        /// <summary>
        /// Computes the batting average for every team in the list over the time period requested.
        /// </summary>
        /// <param name="identifiers">The identifiers.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>IEnumerable&lt;IStatsAck&gt;.</returns>
        private IEnumerable<IStatsAck> ComputeForTeam(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
            foreach (string id in identifiers)
            {
                // Copy for-loop iteration variable to local variable (C# lambda best-practice for loop captures)
                string team = id;

                // One stat entry for every year requested
                foreach (var year in Enumerable.Range(start.Year, (stop.Year - start.Year) + 1))
                {
                    var thisSeason = this.SearchForTeam(team, year);
                    int y = year;       // Lambda capture again

                    // Did we find any batters for team "id" for year "y"?
                    if (thisSeason.Any())
                    {
                        int cumulativeAB = 0, cumulativeH = 0;

                        // Yup, there were batters.  Sum all of the at-bats and all of the hits for all batters for this team for this year.  This makes up the 
                        // team statistic for a single season
                        thisSeason.ToList().ForEach(
                            row =>
                                {
                                    cumulativeAB += row.AB.Value;
                                    cumulativeH += row.H.Value;
                                });

                        // Construct the return object
                        StatsAck thisStat = new StatsAck { Identifier = id, Start = new DateTime(y, 1, 1), Stop = new DateTime(y, 12, 31), Target = StatsTarget.Team };
                        thisStat.Value = BasicStats.BattingAverage(atBats: cumulativeAB, hits: cumulativeH);
                        thisStat.AddMetadataItem("AtBats", cumulativeAB.ToString());
                        thisStat.AddMetadataItem("Hits", cumulativeH.ToString());

                        yield return thisStat;
                    }

                    // Exercise for the reader:  I want one more stat which is the total team batting average over ALL of the years.  That is, if I asked for
                    // stats for 2006, 2007, and 2008, then I want one entry for each of those years (which is the code above) plus one more summary stat for 
                    // all three years.  You do notneed to create a new Linq query for the purpose - all of the data has been generated above, you just need 
                    // to add a little code to save what you need for the summary statistic.  The Start and Stop of the last summary should indicate the entire
                    // range of that summary statistic.  If there is only a single year requested, then I only want the one entry
                    // Fill in the metadata like above, with the total atbats and hits for reference
                }
            }
        }

        /// <summary>
        /// Computes the batting average for the entire league over the time period requested.
        /// </summary>
        /// <param name="identifiers">The identifiers.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>IEnumerable&lt;IStatsAck&gt;.</returns>
        private IEnumerable<IStatsAck> ComputeForLeague(IEnumerable<string> identifiers, DateTime start, DateTime stop)
        {
           // TODO: Not yet implemented.  Will work on later 
            return null;
        }

        /// <summary>
        /// Computes the Batting Average stat for each identifier in the incoming list (might be more than
        /// one player/team/league per invocation).
        /// </summary>
        /// <param name="identifiers">The identifiers list.  Must be a value from the Lahman Master database for player, team, or league</param>
        /// <param name="target">The target. Is this stat for a player, team, or league?</param>
        /// <param name="start">The start timeframe.  For Lahman entries, ONLY THE YEAR PORTION IS SIGNIFICANT</param>
        /// <param name="stop">The stop timeframe. For Lahman entires, ONLY THE YEAR PORTION IS SIGNIFICANT</param>
        /// <returns>IEnumerable&lt;IStatsAck&gt;.</returns>
        public override IEnumerable<IStatsAck> Compute(IEnumerable<string> identifiers, StatsTarget target, DateTime start, DateTime stop)
        {
            switch (target)
            {
                case StatsTarget.Individual:
                    return ComputeForIndividual(identifiers, start, stop);
                case StatsTarget.Team:
                    return ComputeForTeam(identifiers, start, stop);
                case StatsTarget.League:
                    return ComputeForLeague(identifiers, start, stop);
                default: return null;
            }
        }
    }
}
