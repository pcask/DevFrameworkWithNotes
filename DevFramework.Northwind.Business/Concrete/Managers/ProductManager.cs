using AutoMapper;
using DevFramework.Core.Aspects.PostSharp;
using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;
using DevFramework.Core.Aspects.PostSharp.CacheAspects;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using DevFramework.Core.Aspects.PostSharp.TransactionAspects;
using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Utilities.Mappings;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    // İstersek Aspect'lerimizi method, class veya assembly seviyesinde (Properties > AssemblyInfo.cs) uygulayabiliriz.
    // Fakat farkında olmamız gereken durum Aspect'lerimizin her seviye için tekrar yürütülüyor olacağıdır yani en üst seviye tanımlaması olan
    // assembly seviyesindeki tanımlama alt seviyedeki attributeleri ezmeyecektir. Genel işlemler için attributeleri üst seviyelerde tanımlarken
    // specific işlemler için tanımlamaları alt seviyelere çekmeliyiz.
    // [LogAspect(typeof(FileLogger))]
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;

        public ProductManager(IProductDAL productDAL, IMapper mapper)
        {
            _productDAL = productDAL;
            _mapper = mapper;
        }

        [CacheAspect(typeof(MemoryCacheManager), 120)] // Sunucunun önbelleğine 2 saat boyunca kalacak bir cache eklenecektir.
        //[LogAspect(typeof(FileLogger))] // GetAll method'ının dosyaya loglanması için LogAspect uyguluyoruz.
        //[LogAspect(typeof(DatabaseLogger))] // GetAll method'ının Database'e loglanması için LogAspect uyguluyoruz.
        [PerformanceCounterAspect(1)]
        //[SecuredOperationAspect(Roles = "Admin,editor")]
        public List<Product> GetAll()
        {
            //Thread.Sleep(2000); // Current thread'i 2 saniye askıya alıyoruz.
            //return _productDAL.GetList(); Mvc projesinde sorunsuz çalışırken, Api projesinde serialize edilemeyecektir.

            // Api projesinde direk entity'i döndürmek yerine yanlış olmasına rağmen erindiğimden AutoMapper aracılığıyla product nesnesini 
            // tekrar product'a mapliyorum tabi küçük bir farkla navigation property'leri ignore ederek, böylelikle serialize işleminde bir sorun çıkmayacaktır.
            //return AutoMapperHelper.MapToSameTypeList<Product>(_productDAL.GetList());
            // Yukarıdaki gibi generic method'ın hangi tip'ile çalışacağını belirtmeye gerek yok, çünkü geçilen parametreden genereic tip otomatik belirleneniyor.
            //return AutoMapperHelper.MapToSameTypeList(_productDAL.GetList());

            return _mapper.Map<List<Product>>(_productDAL.GetList());

        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public Product GetById(int id)
        {
            return _productDAL.Get(p => p.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        public Product Add(Product product)
        {
            return _productDAL.Add(product);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Update(Product product)
        {
            return _productDAL.Update(product);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(Product product)
        {
            _productDAL.Delete(product);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        public void TransactionalOperation(Product pr1, Product pr2)
        {
            _productDAL.Add(pr1);

            // Some business codes

            _productDAL.Update(pr2);
        }

        //[SecuredOperationAspect(Roles = "Admin,editor")]
        public List<ProductDetail> GetProductsWithDetail()
        {
            return _productDAL.GetProductsWithDetail();
        }
    }
}
