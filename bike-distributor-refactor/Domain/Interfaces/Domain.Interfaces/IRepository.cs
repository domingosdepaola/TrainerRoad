using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T, ID>
    {
        List<T> ListAll();
        T Get(ID id);

        bool Update(T item);

        bool Insert(T item);

        bool Delete(ID id);

        bool DeleteItem(T item);

        List<T> Search(T criterio);
        List<T> Search(string criterio);
    }
}
