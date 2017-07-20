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
using StatsManager;

namespace LahmanStats
{
    /// <summary>
    /// Basic StatsAck.  Implements the IStatsAck interface.  Does not do anything
    /// fancy other than provide concrete implementations of the interface members.
    /// </summary>
    /// <seealso cref="StatsManager.IStatsAck" />
    class StatsAck : IStatsAck
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the target for this stat.
        /// </summary>
        /// <value>The target.</value>
        public StatsTarget Target { get; set; }

        /// <summary>
        /// Gets or sets the start for this stat.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the stop for this stat.
        /// </summary>
        /// <value>The stop.</value>
        public DateTime Stop { get; set; }

        /// <summary>
        /// Gets or sets the stat value.
        /// </summary>
        /// <value>The value.</value>
        public double Value { get; set; }
    }
}
