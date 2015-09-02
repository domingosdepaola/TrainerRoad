using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Domain.Entity;

namespace BikeDistributorDLL
{
    public class BikeDistributor
    {
        public string GenerateOrderWithRecept(Order order, bool htmlRecept) 
        {
            OrderApplication orderApplication = new OrderApplication();
            return orderApplication.orderService.GenerateOrderWithRecept(order, htmlRecept);
        }
    }
}
