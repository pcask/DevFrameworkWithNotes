using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System.Collections.Generic;

namespace DevFramework.Northwind.MvcWebUI
{
    public class ProductListViewModel
    {
        public List<ProductDetail> Products { get; set; }
    }
}