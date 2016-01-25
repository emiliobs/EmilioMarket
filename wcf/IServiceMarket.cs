using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceMarket
    {

        [OperationContract]
        Products GetProduct(int productID);

        [OperationContract]
        List<Products> GetProducts();

        // TODO: Add your service operations here
    }
            
}
