using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VillaProject.Application.Repository;

namespace VillaProject.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        
        private readonly ApplicationDBContext _applicationDBContext;
        public GenericRepository(ApplicationDBContext _applicationDBContext)
        {
            this._applicationDBContext = _applicationDBContext;
        }
        public void Add(T entity)
        {
            _applicationDBContext.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationDBContext.Set<T>().ToList();
        }

        public T GetElementByID(System.Linq.Expressions.Expression<Func<T, bool>>? Predicate = null, int? ID = null)
        {
            if (ID is not null)
            {
                return _applicationDBContext.Set<T>().Find(ID)!;
            }

            return _applicationDBContext.Set<T>().FirstOrDefault(Predicate!)!;
        }

        public void Delete(T entity)
        {
            _applicationDBContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _applicationDBContext.Set<T>().Update(entity);
        }

        public IEnumerable<T> GetVillaNames(string Selector)
        {
            return _applicationDBContext.Set<T>().Include(Selector).ToList();
        }
        public bool CheckExistance(Expression<Func<T, bool>> Predicate)
        {
            return _applicationDBContext.Set<T>().Any(Predicate);
        }
    }
}