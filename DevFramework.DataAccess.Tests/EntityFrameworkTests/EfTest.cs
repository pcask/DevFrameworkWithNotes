using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevFramework.DataAccess.Tests.EntityFrameworkTests
{
    [TestClass]
    public class EfTest
    {
        [TestMethod]
        public void Get_all_returns_all_products()
        {
            EfProductDAL productDAL = new EfProductDAL();

            var result = productDAL.GetList();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_all_with_parameter_returns_filtered_products()
        {
            EfProductDAL productDAL = new EfProductDAL();

            var result = productDAL.GetList(p => p.UnitsInStock < 100);

            Assert.AreEqual(67, result.Count);
        }

        [TestMethod]
        public void Get_all_returns_with_parameter_productDetails()
        {
            EfProductDAL productDAL = new EfProductDAL();

            var result = productDAL.GetProductsWithDetail(p => p.CategoryName == "Beverages");

            Assert.AreEqual(12, result.Count);
        }
    }
}
