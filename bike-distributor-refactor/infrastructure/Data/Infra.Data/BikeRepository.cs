using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class BikeRepository : Repository<Bike,int>, IBikeRepository
    {

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bike> Search(Bike criterio)
        {
            using (TravelRoadContext context = new TravelRoadContext()) 
            {
                var query = from b in context.Bike
                            where (criterio.Brand == null || b.Brand.Contains(criterio.Brand))
                            && (criterio.Model == null || b.Model.Contains(criterio.Model))
                            && (criterio.Price == null || criterio.Price == b.Price)
                            select b;

                return query.ToList();
            }
        }

        public List<Bike> Search(string criterio)
        {
            using (TravelRoadContext context = new TravelRoadContext())
            {
                var query = from b in context.Bike
                            where (b.Brand.Contains(criterio))
                            || (b.Model.Contains(criterio))
                            ||(b.Price.ToString().Contains(criterio))
                            select b;

                return query.ToList();
            }
        }
    }
}
