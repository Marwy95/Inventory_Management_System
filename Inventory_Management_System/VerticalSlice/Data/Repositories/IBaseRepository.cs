using Inventory_Management_System.VerticalSlice.Entities;
using System.Linq.Expressions;

namespace Inventory_Management_System.VerticalSlice.Data.Repositories
{
    public interface IBaseRepository<T> 
    {
        T Add(T entity);
        void Update (T entity);
        void Delete (T entity);
        void Delete(int id);
        T GetById (int id);
        T First(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll ();
        IQueryable<T> Get(Expression<Func<T,bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        void SaveChanges();

    }
}
