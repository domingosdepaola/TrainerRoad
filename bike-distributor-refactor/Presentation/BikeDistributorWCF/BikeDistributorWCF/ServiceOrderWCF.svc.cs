using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Application;
using Domain.Entity;

namespace BikeDistributorWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceOrderWCF" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceOrderWCF.svc or ServiceOrderWCF.svc.cs at the Solution Explorer and start debugging.
    public class ServiceOrderWCF : IServiceOrderWCF
    {
        OrderApplication orderApplication = new OrderApplication();
        public int CreateOrder(Domain.Entity.Order order)
        {
            return orderApplication.orderService.CreateOrder(order);
        }

        public string GetReceipt(int idOrder, bool html)
        {
            if (html)
            {
                return orderApplication.orderService.GetHtmlReceipt(idOrder);
            }
            else
            {
                return orderApplication.orderService.GetReceipt(idOrder);
            }
        }

        public string GenerateOrderWithRecept(Order order, bool htmlRecept)
        {
            return orderApplication.orderService.GenerateOrderWithRecept(order, htmlRecept);
        }
    }
}
