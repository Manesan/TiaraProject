using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using tiara_api.DataContext;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Repository
{
    public class GenCRUDRepository<T> : IGenCRUDRepository<T>
        where T : BaseEntity
    {
        private readonly DataContextDB db;
        public GenCRUDRepository(DataContextDB _db)
        {
            db = _db;
        }

        public Task<T> AddAndReturnAsync(T baseEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(T baseEntity)
        {
            baseEntity.CreatedDate = DateTime.Now;

            await db.Set<T>().AddAsync(baseEntity);
            await db.SaveChangesAsync();
            return baseEntity.Id;
        }

        public async Task BulkUpdateAsync(List<T> entities)
        {
            db.BulkUpdate(entities, new BulkConfig { BatchSize = 5000 });
            await db.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> lines;

            lines = db.Set<T>();

            return lines.AsQueryable();
        }

        public IQueryable<T> GetAll(bool isDeleted, bool isInActive)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByPrimaryKeyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool isDeleted, bool isInactive)
        {
            return db.Set<T>().Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetWhereWithInclude(Expression<Func<T, bool>> predicate, string include)
        {
            return db.Set<T>().Where(predicate).Include(include).AsQueryable();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task TrashAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T baseEntity)
        {
            db.Set<T>().Update(baseEntity);
            await db.SaveChangesAsync();

            return baseEntity;
        }
    }
}
