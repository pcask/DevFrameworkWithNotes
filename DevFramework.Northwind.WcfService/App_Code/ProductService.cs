using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// Business tarafında IProductService'imizi bir SOA servisine dönüştürüyoruz ( [ServiceContract] attribute'ü ile ) ardından
// ProductService nesnemize implement ederek aslında business katmanındaki ProductManager' ile aynı operasyonlara sahip olmasını sağlıyoruz
// Ardından ProductManager'ın bir instance'ını üreterek bu instance üzerinden bütün operasyonları ProductService nesnemizin
// ilgili operasyonlarında çağırıyoruz.
// Aslında burada tek yaptığımız şey ProductManager'ı sarmallamak oluyor.
public class ProductService : IProductService
{
    public ProductService()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    // SOA(Service Oriented Architecture) standartlarına göre parametreli constructor yoktur. Bu yüzden Dependency Injecttion ihtiyaçlarımızı
    // Wcf projelerimizde Factory Pattern yaklaşımıyla bir nesne üzerinden IoC ile karşılayabiliriz.
    private IProductService _productService = InstanceFactory.GetInstance<IProductService>();

    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }

    public Product Add(Product product)
    {
        return _productService.Add(product);
    }
    public Product Update(Product product)
    {
        return _productService.Update(product);
    }
    public void Delete(Product product)
    {
        _productService.Delete(product);
    }

    public Product GetById(int id)
    {
        return _productService.GetById(id);
    }

    public List<ProductDetail> GetProductsWithDetail()
    {
        return _productService.GetProductsWithDetail();
    }

    public void TransactionalOperation(Product pr1, Product pr2)
    {
        _productService.TransactionalOperation(pr1, pr2);
    }
}