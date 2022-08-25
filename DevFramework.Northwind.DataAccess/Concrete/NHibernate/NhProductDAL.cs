using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    public class NhProductDAL : NhEntityRepositoryBase<Product>, IProductDAL
    {
        private readonly NHibernateHelper _helper;
        public NhProductDAL(NHibernateHelper helper) : base(helper)
        {
            _helper = helper;
        }

        public List<ProductDetail> GetProductsWithDetail(Expression<Func<ProductDetail, bool>> filter = null)
        {
            var query = _helper.OpenSession().Query<Product>().Select(p => new ProductDetail
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                CategoryName = p.Category.CategoryName
            }).AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();

        }
    }
}
