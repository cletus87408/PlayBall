// ***********************************************************************
// Assembly         : LahmanStats
// Author           : Bob
// Created          : 07-19-2017
//
// Last Modified By : Bob
// Last Modified On : 07-19-2017
// ***********************************************************************
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using StatsManager;
using WpfApplication1;
using System.Linq;

namespace LahmanStats
{
    /// <summary>
    /// Class LahmanStatsBase.  Base class for LahmanStats derived objects.
    /// The only reason for this class to exist is to provide a container for the
    /// database object.  Since the database object is relatively expensive to create,
    /// each stats class will hold a reference to a stats database supplied through
    /// constructor injection rather than create a new one from scratch for every
    /// stats class.  
    /// 
    /// It also provides default implementations for the metadata object which, for the
    /// time being for most stats, will probably be empty. 
    /// </summary>
    /// <seealso cref="StatsManager.IStatsPlugin" />
    public abstract class LahmanStatsBase : IStatsPlugin
    {
        /// <summary>
        /// The database
        /// </summary>
        protected readonly LahmanEntities database;

        /// <summary>
        /// The metadata
        /// </summary>
        protected Dictionary<string, string> metadata = new Dictionary<string, string>();

        /// <summary>
        /// Gets the name of the stat.  The name should be reasonably descriptive, full language text
        /// Something like "Wins above replacement" would do nicely.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the short name.  This is the acronym or the short form.  Something like "WAR"
        /// </summary>
        /// <value>The short name.</value>
        public abstract string ShortName { get; }

        /// <summary>
        /// Gets the explanation for a stat.  A descriptive string discussing what the stat represents.
        /// This will be used by the stats management gui to provide meaningful data for the user
        /// </summary>
        /// <value>The explanation.</value>
        public abstract string Explanation { get; }

        /// <summary>
        /// Gets the metadata.  Metadata is undefined for a stat, but might provide useful information
        /// for a GUI or a means of supplying stat specific knowledge.  This is mostly a hedge against
        /// unknown future needs (for now).
        /// </summary>
        /// <value>The metadata.</value>
        public IReadOnlyDictionary<string, string> Metadata => metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="LahmanStatsBase"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public LahmanStatsBase(LahmanEntities db)
        {
            this.database = db;
        }

        /// <summary>
        /// Computes the specified stat over the specified timespan.
        /// </summary>
        /// <param name="identifier">The identifier(s).  Identifiers are unique for a given target type.  For instance, if using the Lahman database
        /// "suzukic01" is the unique ID for Ichiro Suzuki, whereas "SEA" is the id for the Seattle Mariners.  This function allows the
        /// consumer to pass an arbitrary number of identifiers.  Some stats are expensive to compute - passing multiple requests in one
        /// might allow for some database access reuse and improved performance.</param>
        /// <param name="target">The target.  Is this request for a player, team, or league?</param>
        /// <param name="start">The start date for the stat.</param>
        /// <param name="stop">The stop date for the stat.  The Lahman database only has entries for each player by year, but as part
        /// of the PlayBall project, we expect to be adding stats on a per-game basis, hence the need for start/stop instead of just
        /// year for the duration</param>
        /// <returns>An enumerable collection of return values, one for each input identifier.</returns>
        public abstract IEnumerable<IStatsAck> Compute(IEnumerable<string> identifier, StatsTarget target,
            DateTime start, DateTime stop);
    }
}
