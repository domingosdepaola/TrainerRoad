using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class OrderLineRepository : Repository<OrderLine,int>,IRepository<OrderLine,int>
    {

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderLine> Search(OrderLine criterio)
        {
            throw new NotImplementedException();
        }

        public List<OrderLine> Search(string criterio)
        {
            throw new NotImplementedException();
        }
    }
}
