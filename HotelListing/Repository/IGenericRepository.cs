using HotelListing.Models;
using System.Linq.Expressions;
using X.PagedList;

namespace HotelListing.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );

        Task<IPagedList<T>> GetAllAsync(
            RequestParams requestParams,
            List<string> includes = null
        );

        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(int id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
    }
}
