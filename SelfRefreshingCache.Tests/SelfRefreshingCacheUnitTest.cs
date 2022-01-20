using Microsoft.Extensions.Logging;
using Moq;
using SelfRefreshingCache.Cache;
using System;
using System.Collections.Generic;
using Xunit;

namespace SelfRefreshingCache.Tests
{
    public class SelfRefreshingCacheUnitTest
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            var mock = new Mock<ILogger<Country>>();
            ILogger<Country> logger = mock.Object;

            //act
            SelfRefreshingCache<List<Country>> src = new SelfRefreshingCache<List<Country>>(logger, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(50), new Country().GetCountriesData);


        }
    }
}
