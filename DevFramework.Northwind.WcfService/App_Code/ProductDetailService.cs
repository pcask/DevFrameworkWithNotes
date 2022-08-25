using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.WcfService.App_Code.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.WcfService.App_Code
{
    public class ProductDetailService : IProductDetailService
    {
        IProductService _productService = InstanceFactory.GetInstance<IProductService>();

        public List<ProductDetail> GetProductsWithDetail()
        {
            return _productService.GetProductsWithDetail();
        }
    }
}
