﻿using Domain.Entity;
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
    }
}
