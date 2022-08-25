using AutoMapper;
using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DevFramework.Northwind.Business.Tests
{
    [TestClass]
    public class ProductManagerTests
    {

        // ValidationException türünden bir hata beklediğimizi belirtiyoruz.
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Product_validation_check()
        {
            Mock<IProductDAL> pMock = new Mock<IProductDAL>();
            Mock<IMapper> mMock = new Mock<IMapper>();
            ProductManager manager = new ProductManager(pMock.Object, mMock.Object);

            // Belirlediğimiz validation kurallarına uymayan bir nesne gönderdiğimiz için Validation Exception hatası bekliyoruz.
            // Add method'ı çalışmadan önce aspect'imizin OnEntry method'ında validation kontrolleri yapılacak ve ValidationException hatası fırlatılacak.
            manager.Add(new Product());
        }
    }
}
