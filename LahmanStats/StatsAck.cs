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
    using System.Collections.Generic;

    /// <summary>
    /// Basic StatsAck.  Implements the IStatsAck interface.  Does not do anything
    /// fancy other than provide concrete implementations of the interface members.
    /// </summary>
    /// <seealso cref="IStatsAck" />
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

        /// <summary>
        /// The metadata for this ack
        /// Uses lazy initialization.  If no one ever assigns any metadata, then
        /// no metadata is created.  
        /// </summary>
        private Dictionary<string, string> metadata;

        /// <summary>
        /// Adds the metadata item.  If this is the first metatada item, then the
        /// metadata collection which did not previously exist is created.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddMetadataItem(string key, string value)
        {
            if (this.metadata == null)
            {
                this.metadata = new Dictionary<string, string>();
            }

            this.metadata[key] = value;
        }

        /// <summary>
        /// Gets the metadata for this ack.
        /// </summary>
        /// <value>The metadata.</value>
        public IReadOnlyDictionary<string, string> Metadata => this.metadata;
    }
}
