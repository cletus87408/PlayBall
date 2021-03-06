﻿using Database;

namespace LahmanStats
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class LahmanEntitiesFieldingExtensions.  Fielding database extension methods to provide convenient filters
    /// </summary>
    static class LahmanEntitiesFieldingExtensions
    { 
        //searches for a matching individual ID in the fielding database and returns all matching rows
        public static IQueryable<Fielding> Individual(this IQueryable<Fielding> data, string id)
        {
            // Find the players entries in the batting database
            return from row in data where row.playerID == id select row;
        }

        public static IQueryable<Fielding> Team(this IQueryable<Fielding> data, string team)
        {
            return data // From all fielders for all time
                .Where(row => row.teamID == team) // Filter out our team only for the result
                .Select(row => row); // Return the entire row
        }

        public static IQueryable<Fielding> League(this IQueryable<Fielding> data, string league)
        {
            return data // From all fielders for all time
                .Where(row => row.lgID == league) // Filter out our league only for the result
                .Select(row => row); // Return the entire row
        }

        public static IQueryable<Fielding> DateRange(this IQueryable<Fielding> data, short startYear, short stopYear)
        {
            // Find those rows from the data entries that match the years requested 
            return from row in data where row.yearID >= startYear where row.yearID <= stopYear select row;
        }
    }
}
