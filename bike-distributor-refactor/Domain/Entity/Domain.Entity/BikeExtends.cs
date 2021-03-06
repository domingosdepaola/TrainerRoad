﻿using Infra.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Domain.Entity
{
    public partial class Bike
    {
        public Bike(string brand, string model, int price)
        {
            Brand = brand;
            Model = model;
            Price = price;
        }
        public NumericValues PriceRange 
        {
            get 
            {
                if (this.Price == null) 
                {
                    return NumericValues.NoDiscount;
                }
                if (this.Price.Value >= (int)NumericValues.OneThousand && this.Price.Value < (int)NumericValues.TwoThousand) 
                {
                    return NumericValues.OneThousand;
                }
                else if (this.Price.Value >= (int)NumericValues.TwoThousand && this.Price.Value < (int)NumericValues.FiveThousand) 
                {
                    return NumericValues.TwoThousand;
                }
                else if (this.Price.Value >= (int)NumericValues.FiveThousand)
                {
                    return NumericValues.FiveThousand;
                }
                else
                {
                    return NumericValues.NoDiscount;
                }
            }
        }
    }
}
