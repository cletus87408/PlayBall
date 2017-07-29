namespace LahmanStats
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using WpfApplication1;

    /// <summary>
    /// Class LahmanDatabasePlayerHelpers.  Extension methods for extracting a player by id and year range from one of the
    /// databases that supports these keys
    /// 
    /// This is why C# Generics are so MONSTROUSLY inferior to their C++ counterpart.  In C++, I could cover every single 
    /// function here with ONE, count 'em ONE template function.   But alas, C# with its horrible strict typing makes this
    /// hard.  I could solve it with dynamic programming, but that uses reflection (slow) and is ugly as sin.  Oh well, perhaps
    /// one day C# will get proper Duck Typing.
    /// </summary>
    static class LahmanDatabaseBattingExtensions
    {
        //searches for a matching individual ID in the batting database and returns all matching rows
        public static IQueryable<Batting> Individual(this IQueryable<Batting> data, string id)
        {
            // Find the players entries in the batting database
            return from row in data where row.playerID == id select row;
        }

        public static IQueryable<Batting> Team(this IQueryable<Batting> data, string team)
        {
            return data // From all batters for all time
                .Where(row => row.teamID == team) // Filter out our team only for the result
                .Select(row => row); // Return the entire row
        }

        //searches for a matching league id in the batting database and returns all matching rows
        public static IQueryable<Batting> League(this IQueryable<Batting> data, string league)
        {
            return data // from all batters all time
                .Where(row => row.lgID == league)
                .Select(row => row);
        }

        public static IQueryable<Batting> DateRange(this IQueryable<Batting> data, short startYear, short stopYear)
        {
            // Find those rows from the data entries that match the years requested 
            return from row in data where row.yearID >= startYear where row.yearID <= stopYear select row;
        }
    }
}
