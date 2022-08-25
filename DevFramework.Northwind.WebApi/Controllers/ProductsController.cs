using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevFramework.Northwind.WebApi.Controllers
{
    // Api'lerde conventinal isimlendirme standardı nesnelerin çoğul halidir.
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // WebApi'da bu şekilde direkt olarak entity'nin kendisini döndürmek aşağıdaki gibi bir hataya sebeb olabilir.
        // The 'ObjectContent`1' type failed to serialize the response body for content type 'application/xml
        // ORM'ler geriye entity'döndürürken yanında serialize edilemeyecek bir nesne de gönderiyorlar, bu nesne bizim entity'ler arasındaki relation'ı
        // belirtmek için kullandığımız navigation property'lerin içerisinde geliyor ve serialize edilemiyor.
        // Bu durum Mvc projelerimizde sorun olmazken Api projelerimizde verilerin serialize edilebilmesi gerekiryor ki verilerimizi xml veya json gibi
        // tüm consumer'ların anlayabileceği ortak bir dille sunabilelim.
        // Hem böyle bir durumla karşılaşmamak için hem de best practice'ler gözetildiğinde hiçbir zaman arayüz'e entity'nin kendisi gönderilmemelidir.
        // Bunun yerine ViewModel gibi entity'nin sadece sunulmak istenen özelliklerine sahip nesneler kullanılmalıdır.
        public List<Product> Get()
        {
            return _productService.GetAll();
        }
    }
}
