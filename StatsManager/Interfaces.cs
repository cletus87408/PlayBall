// ***********************************************************************
// Assembly         : StatsManager
// Author           : Bob Beauchaine        
// Created          : 07-16-2017
//
// Last Modified By : Bob
// Last Modified On : 07-16-2017
// ***********************************************************************
// <copyright file="Interfaces.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace StatsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enum StatsTarget.  Determines what kind of target to which this stat applies.
    /// </summary>
    public enum StatsTarget
    {
        /// <summary>
        /// An individual stat, applicable to a single person
        /// </summary>
        Individual,

        /// <summary>
        /// A team stat, applicable to all of the members of a team
        /// </summary>
        Team,

        /// <summary>
        /// A league-wide stat
        /// </summary>
        League
    }

    /// <summary>
    /// Interface StatsReturnType.  This is the class that the stats computations will return
    /// </summary>
    public interface IStatsAck
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Identifier { get; }

        /// <summary>
        /// Gets the target for this stat.
        /// </summary>
        /// <value>The target.</value>
        StatsTarget Target { get; }

        /// <summary>
        /// Gets the start for this stat.
        /// </summary>
        /// <value>The start.</value>
        DateTime Start { get; }

        /// <summary>
        /// Gets the stop for this stat.
        /// </summary>
        /// <value>The stop.</value>
        DateTime Stop { get; }

        /// <summary>
        /// Gets the stat value.
        /// </summary>
        /// <value>The value.</value>
        double Value { get; }

        /// <summary>
        /// Gets the metadata for this ack.
        /// </summary>
        /// <value>The metadata.</value>
        IReadOnlyDictionary<string, string> Metadata { get; }
    }

    /// <summary>
    /// Interface Interfaces
    /// </summary>
    public interface IStatsPlugin
    {
        /// <summary>
        /// Gets the name of the stat.  The name should be reasonably descriptive, full language text
        /// Something like "Wins above replacement" would do nicely.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the short name.  This is the acronym or the short form.  Something like "WAR" 
        /// </summary>
        /// <value>The short name.</value>
        string ShortName { get; }

        /// <summary>
        /// Gets the explanation for a stat.  A descriptive string discussing what the stat represents.
        /// This will be used by the stats management gui to provide meaningful data for the user
        /// </summary>
        /// <value>The explanation.</value>
        string Explanation { get; }

        /// <summary>
        /// Gets the metadata.  Metadata is undefined for a stat, but might provide useful information
        /// for a GUI or a means of supplying stat specific knowledge.  This is mostly a hedge against
        /// unknown future needs (for now).
        /// </summary>
        /// <value>The metadata.</value>
        IReadOnlyDictionary<string, string> Metadata { get; }

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
        IEnumerable<IStatsAck> Compute(IEnumerable<string> identifier, StatsTarget target, DateTime start, DateTime stop);
    }
}
