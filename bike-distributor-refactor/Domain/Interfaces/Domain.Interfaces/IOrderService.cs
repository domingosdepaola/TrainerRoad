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
        string GetReceipt(int idOrder);
        string GetHtmlReceipt(int idOrder);
        int CreateOrder(Order order);
        String GenerateOrderWithRecept(Order order,bool htmlRecept);
    }
}
