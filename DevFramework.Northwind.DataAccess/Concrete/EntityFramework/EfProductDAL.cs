using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDAL : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDAL
    {

        // Burada sadece Product operasyonlarına özgü member'lar yer alacak. 
        public List<ProductDetail> GetProductsWithDetail(Expression<Func<ProductDetail, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Eager Loadding
                var query = context.Products.Include("Categories").Select(p => new ProductDetail
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    QuantityPerUnit = p.QuantityPerUnit,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock
                }).AsQueryable();

                if (filter != null)
                    query = query.Where(filter);


                // Linq Query Syntax
                //var result = from p in context.Products
                //             join c in context.Categories on p.CategoryId equals c.CategoryId
                //             select new ProductDetail
                //             {
                //                 ProductId = p.ProductId,
                //                 ProductName = p.ProductName,
                //                 CategoryName = p.Category.CategoryName,
                //                 QuantityPerUnit = p.QuantityPerUnit,
                //                 UnitPrice = p.UnitPrice,
                //                 UnitsInStock = p.UnitsInStock
                //             };

                return query.ToList();

                
            }
        }
    }
}
