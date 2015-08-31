using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        string DoReceipt(int idOrder);
        string DoHtmlReceipt(int idOrder);
        int CreateOrder(Order order);
        String GenerateOrderWithRecept(Order order,bool htmlRecept);
    }
}
