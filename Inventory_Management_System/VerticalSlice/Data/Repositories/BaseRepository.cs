using Inventory_Management_System.VerticalSlice.Entities;
using System.Linq.Expressions;

namespace Inventory_Management_System.VerticalSlice.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _context;
        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            _context.Add(entity);
            return entity;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Any(predicate);
        }

    

        public T First(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(t => !t.IsDeleted);
        }

        public T GetById(int id)
        {
           return GetAll().FirstOrDefault(e=>e.ID == id);
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }
        public void SaveChanges()
        {
           _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
