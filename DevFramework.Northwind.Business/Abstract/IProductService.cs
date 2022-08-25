using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Abstract
{
    // SOA yapısında nesnelerimizin ve içerdikleri member'ların ele alınabilmesi için bazı contract'larla imzalanmış olmaları gerekir.
    // nesnelerimize [ServiceContract], method'larımızı da [OperationContract] attribute'lerini kazandırmalıyız.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<Product> GetAll();
        [OperationContract]
        Product GetById(int id);
        [OperationContract]
        Product Add(Product product);
        [OperationContract]
        Product Update(Product product);
        [OperationContract]
        void Delete(Product product);
        [OperationContract]
        List<ProductDetail> GetProductsWithDetail();
        [OperationContract]
        void TransactionalOperation(Product pr1, Product pr2);
    }
}
