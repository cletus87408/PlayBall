using System;
using System.Collections.Generic;
using System.Collections;
using StatsManager;
using System.Linq;

namespace LahmanStats
{
    public class Cache
    {
        public Dictionary<int, IEnumerable<IStatsAck>> dataCache = new Dictionary<int, IEnumerable<IStatsAck>>();

        //adds a database object to the hashtable cache
        public void Add(IEnumerable<string> identifier, IEnumerable<IStatsAck> value, DateTime startYear, DateTime stopYear)
        {
            int hashCode = HashFunction(identifier, startYear, stopYear);

            //if dictionary already contains this entry, delete it and replace with new data
            if (dataCache.ContainsKey(hashCode))
                dataCache.Remove(hashCode);

            dataCache.Add(hashCode, value);
        }

        //finds a matching hash code in the dictionary and returns it, returns null for failure
        public IEnumerable<IStatsAck> Retrieve(IEnumerable<string> identifier, DateTime startYear, DateTime stopYear)
        {
            int hashCode = HashFunction(identifier, startYear, stopYear);

            //check that entry exists
            if (dataCache.ContainsKey(hashCode))
            {
                return dataCache[hashCode];
            }

            //if it doesn't, return null as failure 
            else
                return null;
        }

        //convert and build a key for use with hash table
        public int HashFunction(IEnumerable<string> identifier, DateTime startYear, DateTime stopYear)
        {
            string key= string.Join(",", identifier);
            string StYear = startYear.Year.ToString();
            string SpYear = stopYear.Year.ToString();

            key += StYear;
            key += SpYear;
            int hashCode = key.GetHashCode();

            return hashCode;
        }


        //checks for a matching key in the cache, returns true for match false for no match
        public bool ContainsKey(IEnumerable<string> identifier, DateTime startYear, DateTime stopYear)
        {
            int key = HashFunction(identifier, startYear, stopYear);

            if (dataCache.ContainsKey(key))
                return true;
            else
                return false;
        }
        //wrapper method
        public void Clear()
        {
            dataCache.Clear();
        }
    }
}
