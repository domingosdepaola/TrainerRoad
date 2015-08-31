using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Application;

namespace BikeDistributorWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceOrderWCF" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceOrderWCF.svc or ServiceOrderWCF.svc.cs at the Solution Explorer and start debugging.
    public class ServiceOrderWCF : IServiceOrderWCF
    {
        BikeApplication bikeApplication;
        public void DoWork()
        {
        }


        public int CreateOrder(Domain.Entity.Order order)
        {
            bikeApplication = new BikeApplication();
            return bikeApplication.orderService.CreateOrder(order);
        }

        public string GetReceipt(int idOrder)
        {
            throw new NotImplementedException();
        }

        public string GetHtmlReceipt(int idOrder)
        {
            throw new NotImplementedException();
        }
    }
}
