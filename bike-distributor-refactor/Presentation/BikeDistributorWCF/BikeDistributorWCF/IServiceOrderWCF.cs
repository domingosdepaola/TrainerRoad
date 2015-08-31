using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain.Entity;

namespace BikeDistributorWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceOrderWCF" in both code and config file together.
    [ServiceContract]
    public interface IServiceOrderWCF
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        int CreateOrder(Order order);

        [OperationContract]
        String GetReceipt(int idOrder);

        [OperationContract]
        String GetHtmlReceipt(int idOrder);
    }
}
