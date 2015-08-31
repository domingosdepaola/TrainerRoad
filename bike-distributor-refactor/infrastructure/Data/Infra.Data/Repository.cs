using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class Repository<T,ID> where T : class
    {

        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public List<T> ListAll()
        {
            using (TravelRoadContext context = new TravelRoadContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public virtual bool Update(T item)
        {
            try
            {
                using (TravelRoadContext context = new TravelRoadContext())
                {
                    context.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
                    return context.SaveChanges() > 0;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex,ex.Message,null);
                return false;
            }
        }
        public T Get(ID id) 
        {
            try
            {
                using (TravelRoadContext context = new TravelRoadContext()) 
                {
                    return context.Set<T>().Find(id);
                }
            }
            catch (Exception ex) 
            {
                log.Error(ex, ex.Message, null);
                return null;
            }
        }
        public bool Insert(T item)
        {
            try
            {
                using (TravelRoadContext context = new TravelRoadContext())
                {
                    context.Entry<T>(item).State = System.Data.Entity.EntityState.Added;
                    return context.SaveChanges() > 0;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex, ex.Message, null);
                return false;
            }
        }

        public virtual bool DeleteItem(T item)
        {
            try
            {
                using (TravelRoadContext context = new TravelRoadContext())
                {
                    context.Entry<T>(item).State = System.Data.Entity.EntityState.Deleted;
                    return context.SaveChanges() > 0;
                }
            }
            catch(Exception ex)
            {
                log.Error(ex, ex.Message, null);
                return false;
            }
        }
    }
}
