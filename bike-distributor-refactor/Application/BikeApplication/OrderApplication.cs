using Domain.Interfaces;
using Domain.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class OrderApplication : BaseApplication
    {
        public IOrderService orderService;
        public OrderApplication() 
        {
            orderService = Factory.Instance.Resolve<IOrderService>();
        }
    }
}
