using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DevFramework.DataAccess.Tests.NHibernateTests
{
    [TestClass]
    public class NhTest
    {
        [TestMethod]
        public void Get_all_returns_all_products()
        {
            NhProductDAL productDAL = new NhProductDAL(new SqlServerHelper());

            var result = productDAL.GetList();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_all_returns_with_parameter_filtered_products()
        {
            NhProductDAL productDAL = new NhProductDAL(new SqlServerHelper());

            var result = productDAL.GetList(p => p.UnitsInStock < 100);

            Assert.AreEqual(67, result.Count);
        }

        [TestMethod]
        public void Get_all_returns_all_categories()
        {
            NhCategoryDAL categoryDAL = new NhCategoryDAL(new SqlServerHelper());

            var result = categoryDAL.GetList();

            Assert.AreEqual(8, result.Count);
        }

        [TestMethod]
        public void Get_all_returns_with_parameter_filtered_productDetail()
        {
            NhProductDAL productDAL = new NhProductDAL(new SqlServerHelper());

            var result = productDAL.GetProductsWithDetail(p => p.CategoryName == "Beverages");

            Assert.AreEqual(12, result.Count);
        }
    }
}
