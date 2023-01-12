using MainProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace MainProject.DAC
{
    public class BaseRepository<Type> : IRepository<Type> where Type : class
    {
        protected Context _context;
        protected DbSet<Type> Entities;
        public BaseRepository()
        {


        }
        public int Save()
        {

            using (var newContext = new Context())
            {
               return newContext.SaveChanges();
            }


        }

        public void Add(Type entity)
        {
            using (var newContext = new Context())
            {
                newContext.Set<Type>().Add(entity);
                newContext.SaveChanges();
            }
                
            

        }


        
        public void Remove(Type entity)
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                Entities.Attach(entity);
                Entities.Remove(entity);
                newContext.SaveChanges();
            }
            
        }

        public virtual IEnumerable<Type> Get(Expression<Func<Type, bool>> filter = null, Func<IQueryable<Type>, IOrderedQueryable<Type>> orderby = null, string includeProperties = "")
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                IQueryable<Type> query = Entities;
                if (filter != null)
                {
                    query.Where(filter);
                }
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperties);
                }
                if (orderby != null)
                {
                    return orderby(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            
        }
        public IEnumerable<Type> Include(params Expression<Func<Type, object>>[] includes)
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                IQueryable<Type> query = Entities.Include(includes[0]);
                foreach (Expression<Func<Type, object>> include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return query.ToList();
            }
            
        }
        

        public Type Get(int id)
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                return Entities.Find(id);
            }
            
        }

        public IEnumerable<Type> GetAll()
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                return Entities.ToList();
            }
            
        }

        public Type Find(Expression<Func<Type, bool>> predicate)
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                return Entities.Where(predicate).FirstOrDefault();
            }

        }


        public Type SingleOrDefault(Expression<Func<Type, bool>> predicate)
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                return Entities.Where(predicate).SingleOrDefault();
            }
            
        }

        public Type FirstOrDefault()
        {
            using (var newContext = new Context())
            {
                Entities = newContext.Set<Type>();
                return Entities.FirstOrDefault();
            }
            
        }
        public DbSet<Type> GetDbContext()
        {
            var newContext = new Context();
                return newContext.Set<Type>();
        }

    }
}