using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using ThirdPartyApiCaller.Models;
using ThirdPartyApiCaller.Utility;
using WeatherApp.Data;
using WeatherApp.Services;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mock = new Mock<IUserService>();

            mock.Setup(m => m.GetUserSearchHistory(1)).Returns(new List<SearchHistory>() { new SearchHistory() { MemberId = 1, ZipCode = "99123" } });

            var result = mock.Object.GetUserSearchHistory(1);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("99123", result[0].ZipCode);
        }
    }
}