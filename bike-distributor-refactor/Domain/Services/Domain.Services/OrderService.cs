using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        public OrderService() 
        {
        
        }
        public string Receipt()
        {
            throw new NotImplementedException();
        }

        public string HtmlReceipt()
        {
            throw new NotImplementedException();
        }
    }
}
