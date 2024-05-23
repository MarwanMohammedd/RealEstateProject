using System.Linq.Expressions;

namespace VillaProject.Application.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetElementByID(Expression<Func<T, bool>>? Predicate = null, int? ID = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetVillaNames(string Selector);
        bool CheckExistance(Expression<Func<T,bool>>Predicate);
    }
}