﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public partial class Order
    {
        public double DefaultTaxRate
        {
            get
            {
                return .0725d;
            }
        }
        public Order(String Company)
        {
            this.Company = Company;
        }
        public void AddLine(Line line) 
        {
            if (this.Line == null) 
            {
                this.Line = new List<Line>();
            }
            this.Line.Add(line);
        }
    }
}
