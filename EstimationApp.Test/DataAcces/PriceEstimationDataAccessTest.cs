using System;
using EstimationApp.DataAccess;
using NUnit.Framework;

namespace EstimationApp.Test.DataAcces
{
    public class PriceEstimationDataAccessTest
    {
        PriceEstimationDataAccess _dataAccess;

        [SetUp]
        public void Init()
        {
            _dataAccess = new PriceEstimationDataAccess();
        }

        [Test]
        public void CalculatePrice_WhenUnitPriceIsNullOrEmpty_TestAsync()
        {
            Assert.CatchAsync<ArgumentException>(async () => await _dataAccess.CalculatePrice(null));
        }
    }
}
