using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class LineRepository : Repository<Line,int>,IRepository<Line,int>
    {

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Line> Search(Line criterio)
        {
            throw new NotImplementedException();
        }

        public List<Line> Search(string criterio)
        {
            throw new NotImplementedException();
        }
    }
}
