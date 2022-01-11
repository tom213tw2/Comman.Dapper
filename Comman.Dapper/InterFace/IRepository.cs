using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Comman.Dapper
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Get(TKey tKey, int? commandTimeout);
        Task<TEntity> GetAsync(TKey id, int? commandTimeout = null);
        IEnumerable<TEntity> GetList();
        IEnumerable<TEntity> GetList(object whereConditions, int? commandTimeout = null);
        IEnumerable<TEntity> GetList(string conditions, object parameters = null, int? commandTimeout = null);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<IEnumerable<TEntity>> GetListAsync(object whereConditions, int? commandTimeout = null);
        Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null, int? commandTimeout = null);
    }
}