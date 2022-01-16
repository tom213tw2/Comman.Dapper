using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Comman.Dapper
{
    /// <summary>
    /// Dapper 介面
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
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
        int Execute(string sql, DynamicParameters param, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteAsync(string sql, DynamicParameters param, int? commandTimeout = null, CommandType? commandType = null);
        IEnumerable<TEntity> Query(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        TEntity QuerySingleOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);
        IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, int? commandTimeout = null);
        Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, int? commandTimeout = null);
        int RecordCount(string conditions = "", object parameters = null, int? commandTimeout = null);
        Task<int> RecordCountAsync(string conditions = "", object parameters = null, int? commandTimeout = null);
        TKey Insert(TEntity entityToInsert, int? commandTimeout = null);
        Task<TKey> InsertAsync(TEntity tEntity, int? commandTimeout = null);
        int Update(TEntity entityToUpdate, int? commandTimeout = null);
        Task<int> UpdateAsync(TEntity entityToUpdate, int? commandTimeout = null, CancellationToken? token = null);

        int Delete(TEntity entityToDelete, int? commandTimeout = null);
        int Delete(TKey id, int? commandTimeout = null);
        Task<int> DeleteAsync(TEntity entityToDelete, int? commandTimeout = null);
        Task<int> DeleteAsync(TKey id, int? commandTimeout = null);
        int DeleteList(string conditions, object parameters = null, int? commandTimeout = null);
      Task<int> DeleteListAsync(string conditions, object parameters = null, int? commandTimeout = null);
    }
}