using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RentOfEquipment.Test
{
    [TestClass]
    public class Calculation_Tests
    {
        [TestMethod]
        public void CalculationTotalCost_CorrectData_ReturnTrue()
        {
            //AVERAGE
            DateTime startTime =Convert.ToDateTime("06/04/2022");
            DateTime endTime = Convert.ToDateTime("10/04/2022");
            double cost = 250;
            decimal RealResult = Convert.ToDecimal(62.5);
            //ACT
            decimal FactialResult = LibbraryCalculate.ClientsServiceClass.TotalCost(startTime, endTime, cost);
            //ASSERT
            Assert.AreEqual(RealResult, FactialResult);
        }

        [TestMethod]
        public void CalculationTotalCost_EqualDate_ReturnTrue()
        {
            //AVERAGE
            DateTime startTime = Convert.ToDateTime("06/04/2022");
            DateTime endTime = Convert.ToDateTime("06/04/2022");
            double cost = 250;
            decimal RealResult = Convert.ToDecimal(12.5);
            //ACT
            decimal FactialResult = LibbraryCalculate.ClientsServiceClass.TotalCost(startTime, endTime, cost);
            //ASSERT
            Assert.AreEqual(RealResult, FactialResult);
        }

        [TestMethod]
        public void CalculationTotalCost_ZeroCost_ReturnTrue()
        {
            //AVERAGE
            DateTime startTime = Convert.ToDateTime("06/04/2022");
            DateTime endTime = Convert.ToDateTime("06/04/2022");
            double cost = 0;
            decimal RealResult = Convert.ToDecimal(0);
            //ACT
            decimal FactialResult = LibbraryCalculate.ClientsServiceClass.TotalCost(startTime, endTime, cost);
            //ASSERT
            Assert.AreEqual(RealResult, FactialResult);
        }
    }
}
