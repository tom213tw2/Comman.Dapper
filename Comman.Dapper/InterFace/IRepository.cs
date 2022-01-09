using System.Collections.Generic;
using System.Data;

namespace Comman.Dapper
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        public TEntity Get(TKey tKey, int? commandTimeout);
        public IEnumerable<TEntity> GetList(object whereConditions, int? commandTimeout);
    }
}