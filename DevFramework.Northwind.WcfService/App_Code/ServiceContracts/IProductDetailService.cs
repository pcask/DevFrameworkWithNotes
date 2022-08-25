using DevFramework.Northwind.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.WcfService.App_Code.ServiceContracts
{

    [ServiceContract]
    public interface IProductDetailService
    {
        [OperationContract]
        List<ProductDetail> GetProductsWithDetail();
    }
}
