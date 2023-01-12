using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace MainProject.DAC
{
    public interface IRepository<Type> where Type : class
    {
        IEnumerable<Type> Get(Expression<Func<Type, bool>> filter = null, Func<IQueryable<Type>, IOrderedQueryable<Type>> orderby = null,
            string includeProperties = "");
        Type Get(int id);
        IEnumerable<Type> GetAll();
        Type Find(System.Linq.Expressions.Expression<Func<Type,bool>> predicate);
        Type SingleOrDefault(System.Linq.Expressions.Expression<Func<Type, bool>> predicate);
        Type FirstOrDefault();
        void Add(Type entity);
        void Remove(Type entity);
        int Save();
    }
}