﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order,int>
    {
        int InsertWithIdReturn(Order order);
    }
}
