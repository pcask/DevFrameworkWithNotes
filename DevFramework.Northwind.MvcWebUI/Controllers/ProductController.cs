using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetProductsWithDetail()
            };

            return View(model);
        }


        public string Add()
        {
            _productService.Add(new Product
            {
                CategoryId = 1,
                ProductName = "Notebook",
                QuantityPerUnit = "1x1",
                UnitPrice = 85000,
                UnitsInStock = 13
            });

            return "Added";
        }


        public string AddUpdate()
        {
            // ProductValidator'a göre iki üründe sorunsuz olduğu için ilk önce DB'e 1. ürün eklenecek ve ardından 2. ürün bulunamadığı için
            // güncelleme işlemi başarısız olup bütün operasyon en başa alınacak ve 1. ürün tekrar silinecektir.

            _productService.TransactionalOperation(new Product
            {
                CategoryId = 5,
                ProductName = "Backpack",
                QuantityPerUnit = "1x1",
                UnitPrice = 1500,
                UnitsInStock = 52
            },
            new Product
            {
                ProductId = 1000, // Update işlemi için gönderilen bu ürün DB'de yok.
                CategoryId = 5,
                ProductName = "Headphone",
                QuantityPerUnit = "1x1",
                UnitPrice = 2500,
                UnitsInStock = 28
            });


            return "Completed";
        }
    }
}
