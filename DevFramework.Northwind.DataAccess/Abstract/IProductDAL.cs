using DevFramework.Core.DataAccess;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    public interface IProductDAL : IEntityRepository<Product>
    {
        // Burada sadece Product operasyonlarına özgü method imzaları yer alacak. 
        List<ProductDetail> GetProductsWithDetail(Expression<Func<ProductDetail, bool>> filter = null);
    }
}
