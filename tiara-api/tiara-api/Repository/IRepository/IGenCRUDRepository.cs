using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tiara_api.Models;

namespace tiara_api.Repository.IRepository
{
    public interface IGenCRUDRepository<T> where T : BaseEntity
    {
        Task<T> GetByPrimaryKeyAsync(int id);
        Task<int> AddAsync(T baseEntity);
        Task<T> AddAndReturnAsync(T baseEntity);
        Task<T> UpdateAsync(T baseEntity);
        Task RemoveAsync(int id);
        Task TrashAsync(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(bool isDeleted, bool isInActive);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool isDeleted, bool isInactive);
        IQueryable<T> GetWhereWithInclude(Expression<Func<T, bool>> predicate, string include);
        Task BulkUpdateAsync(List<T> entities);
    }
}
