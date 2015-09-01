using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Common.Enum;

namespace Domain.Entity
{
    public partial class Bike
    {
        public NumericValues PriceRage 
        {
            get 
            {
                if (this.Price >= (int)NumericValues.OneThousand && this.Price < (int)NumericValues.TwoThousand) 
                {
                    return NumericValues.OneThousand;
                }
                else if (this.Price >= (int)NumericValues.TwoThousand && this.Price < (int)NumericValues.FiveThousand) 
                {
                    return NumericValues.TwoThousand;
                }
                else if (this.Price >= (int)NumericValues.FiveThousand)
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
