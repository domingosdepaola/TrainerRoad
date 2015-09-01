using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class OrderRepository : Repository<Order,int>, IOrderRepository
    {
        public override Order Get(int id)
        {
            using (TravelRoadContext context = new TravelRoadContext()) 
            {
                return context.Order.Include("Line").Include("Line.Bike").Where(x => x.Id == id).FirstOrDefault();
            }
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> Search(Order criterio)
        {
            throw new NotImplementedException();
        }

        public List<Order> Search(string criterio)
        {
            throw new NotImplementedException();
        }

        public int InsertWithIdReturn(Order order)
        {
            using (TravelRoadContext context = new TravelRoadContext()) 
            {
                context.Order.Add(order);
                context.SaveChanges();
                return order.Id;
            }
        }
    }
}
