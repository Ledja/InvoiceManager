using System.Collections.Generic;

namespace SelfRefreshingCache.Tests
{
    internal  class Country
    {
        internal  string CountryName  { get; set; }
        internal  string CountryCode  { get; set; }

        internal List<Country> GetCountriesData()
        {
            return new List<Country>
            {
                new Country(){CountryCode ="AL", CountryName ="Albania"},
                new Country(){CountryCode ="CZ", CountryName ="Czechia"},
                new Country(){CountryCode ="KS", CountryName ="Kosovo"}
            };
        }
    }
}
