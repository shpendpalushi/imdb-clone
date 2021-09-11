using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.Definitions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;

namespace IMDBClone.Domain.Service.Contracts
{
    /// <summary>
    /// Contract for declaring generalized methods
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Verifies if the specified entity exists
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>bool</returns>
        Task<bool> AnyAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Verifies if some entity exists based on some condition
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>bool</returns>
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression = null) where TEntity : class;
        /// <summary>
        /// Adds or updates a specified entity
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>Result</returns>
        Result AddOrUpdate<TEntity>(TEntity entity) where TEntity : class;
        
        /// <summary>
        /// Adds or updates a specified entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>Result</returns>
        Task<Result> AddOrUpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Adds range of entities asynchronously
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>Result</returns>
        Task<Result> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// Updates a range of entities asynchronously //<see cref="IAsyncResult"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>Result</returns>
        Task<Result> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        
        /// <summary>
        /// Gets an entity based on id asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity, TKey>(TKey id) where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(Guid id, Expression<Func<TEntity, TEntity>> selectExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null)
            where TEntity : class;

        Task<TEntity> FirstOrDefaultAsNoTrackingAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TEntity>> selectExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null)
            where TEntity : class;

        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, TEntity>> selectExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null)
            where TEntity : class;

        public TEntity GetAsNoTracking<TEntity>(Guid id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null)
            where TEntity : class;

        Task<TEntity> GetAsNoTrackingAsync<TEntity>(
            Guid id,
            Expression<Func<TEntity, TEntity>> selectExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null,
            bool asSplitQuery = false) where TEntity : class;

        Task<List<TEntity>> GetAllAsNoTrackingAsync<TEntity>(
            Expression<Func<TEntity, bool>> whereExpression = null,
            Expression<Func<TEntity, TEntity>> selectExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null,
            List<Expression<Func<TEntity, object>>> orderExpression = null,
            bool byDescending = false,
            int? take = null,
            int? skip = null,
            bool asSplitQuery = false) where TEntity : class;

        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression = null) where TEntity : class;

        List<TEntity> GetAll<TEntity>() where TEntity : class;

        Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression = null)
            where TEntity : class;

        Task<IdentityResult> RemoveAsync<TEntity, TKey>(TKey id) where TEntity : class;

        Task<IdentityResult> RemoveAsync<TEntity>(TEntity entity) where TEntity : class;

        Task<Result> SoftRemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<Result> SoftRemoveByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity;
        Task<bool> SoftRemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
        Task<Result> RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        Result RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Detach(object entity);


    }
}