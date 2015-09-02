using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    //used to not overloading the entity contructor for serializing the list
    public class LineInstance
    {
        public static Line NewLine(Bike bike, int quantity)
        {
            Line line = new Entity.Line();
            line.Bike = bike;
            line.Quantity = quantity;
            return line;
        }

    }
}
