using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Comman.Dapper
{
    public class DapperRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly IDbConnection Conn;
        protected readonly IRepositoryContext Context;

        public DapperRepository(IRepositoryContext context)
        {
            Context = context;
            Conn = Context.Conn;
        }
        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>By default filters on the Id column</para>
        /// <para>-Id column name can be overridden by adding an attribute on your primary key property [Key]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a single entity by a single id from table T</para>
        /// </summary>
        /// <param name="tKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a single entity by a single id from table TEntity</returns>
        public TEntity Get(TKey tKey, int? commandTimeout= null) => Conn.Get<TEntity>(tKey, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name asynchronously </para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>By default filters on the Id column</para>
        /// <para>-Id column name can be overridden by adding an attribute on your primary key property [Key]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a single entity by a single id from table T</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a single entity by a single id from table TEntity</returns>
        public async Task<TEntity> GetAsync(TKey id, int? commandTimeout = null) => await Conn.GetAsync<TEntity>(id, Context.Tran, commandTimeout=null);
        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a list of all entities</para>
        /// </summary>
        /// <returns>Gets a list of all entities</returns>
        public IEnumerable<TEntity> GetList() => Conn.GetList<TEntity>();
        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional exact match where conditions</returns>
        public IEnumerable<TEntity> GetList(object whereConditions, int? commandTimeout = null) => Conn.GetList<TEntity>(whereConditions, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause and/or order by clause ex: "where name='bob'" or "where age&gt;=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <returns>Gets a list of entities with optional SQL where conditions</returns>
        public IEnumerable<TEntity> GetList(string conditions, object parameters = null, int? commandTimeout = null) => Conn.GetList<TEntity>(conditions, parameters, Context.Tran, commandTimeout);
        
        /// <summary>
        /// <para>By default queries the table matching the class name asynchronously</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a list of all entities</para>
        /// </summary>
        /// <returns>Gets a list of all entities</returns>
        public async Task<IEnumerable<TEntity>> GetListAsync() => await Conn.GetListAsync<TEntity>();

        /// <summary>
        /// <para>By default queries the table matching the class name asynchronously</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <returns>Gets a list of entities with optional exact match where conditions</returns>
        public async Task<IEnumerable<TEntity>> GetListAsync(object whereConditions, int? commandTimeout = null) => await Conn.GetListAsync<TEntity>(whereConditions, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause and/or order by clause ex: "where name='bob'" or "where age&gt;=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional SQL where conditions</returns>
        public async Task<IEnumerable<TEntity>> GetListAsync(string conditions, object parameters = null, int? commandTimeout = null) => await Conn.GetListAsync<TEntity>(conditions, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(string sql, DynamicParameters param, int? commandTimeout = null, CommandType? commandType = null) => Conn.Execute(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> ExecuteAsync(string sql, DynamicParameters param, int? commandTimeout = null, CommandType? commandType = null) => await Conn.ExecuteAsync(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// Executes a query, returning the data typed as <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>/
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public IEnumerable<TEntity> Query(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => Conn.Query<TEntity>(sql, param, Context.Tran, commandTimeout: commandTimeout, commandType: commandType);

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public TEntity QueryFirstOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => Conn.QueryFirstOrDefault<TEntity>(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public async Task<TEntity> QueryFirstOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => await Conn.QueryFirstOrDefaultAsync<TEntity>(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public TEntity QuerySingleOrDefault(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => Conn.QuerySingleOrDefault<TEntity>(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="TEntity">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public async Task<TEntity> QuerySingleOrDefaultAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => await Conn.QuerySingleOrDefaultAsync<TEntity>(sql, param, Context.Tran, commandTimeout, commandType);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age" - not required </para>
        /// <para>orderby is a column or list of columns to order by ex: "lastname, age desc" - not required - default is by primary key</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a paged list of entities with optional exact match where conditions</returns>
        public IEnumerable<TEntity> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string @orderby, object parameters = null, int? commandTimeout = null) => Conn.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, @orderby, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age" - not required </para>
        /// <para>orderby is a column or list of columns to order by ex: "lastname, age desc" - not required - default is by primary key</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional exact match where conditions</returns>
        public async Task<IEnumerable<TEntity>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string @orderby, object parameters = null, int? commandTimeout = null) => await Conn.GetListPagedAsync<TEntity>(pageNumber, rowsPerPage, conditions, @orderby, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a number of records entity by a single id from table T</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age" - not required </para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public int RecordCount(string conditions = "", object parameters = null, int? commandTimeout = null) => Conn.RecordCount<TEntity>(conditions, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age" - not required </para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public async Task<int> RecordCountAsync(string conditions = "", object parameters = null, int? commandTimeout = null) => await Conn.RecordCountAsync<TEntity>(conditions, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Inserts a row into the database, using ONLY the properties defined by TEntity</para>
        /// <para>By default inserts into the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</para>
        /// </summary>
        /// <param name="entityToInsert"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</returns>
        public TKey Insert(TEntity entityToInsert, int? commandTimeout = null) => Conn.Insert<TKey, TEntity>(entityToInsert, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Inserts a row into the database, using ONLY the properties defined by TEntity</para>
        /// <para>By default inserts into the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</para>
        /// </summary>
        /// <param name="tEntity"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</returns>
        public async Task<TKey> InsertAsync(TEntity tEntity, int? commandTimeout = null) => await Conn.InsertAsync<TKey, TEntity>(tEntity, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Updates a record or records in the database with only the properties of TEntity</para>
        /// <para>By default updates records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Updates records where the Id property and properties with the [Key] attribute match those in the database.</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns number of rows affected</para>
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of affected records</returns>
        public int Update(TEntity entityToUpdate, int? commandTimeout = null) => Conn.Update(entityToUpdate, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Updates a record or records in the database asynchronously</para>
        /// <para>By default updates records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Updates records where the Id property and properties with the [Key] attribute match those in the database.</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns number of rows affected</para>
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of affected records</returns>
        public async Task<int> UpdateAsync(TEntity entityToUpdate, int? commandTimeout = null, CancellationToken? token = null) => await Conn.UpdateAsync(entityToUpdate, Context.Tran, commandTimeout, token);

        /// <summary>
        /// <para>Deletes a record or records in the database that match the object passed in</para>
        /// <para>-By default deletes records in the table matching the class name</para>
        /// <para>Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the number of records affected</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityToDelete"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public int Delete(TEntity entityToDelete, int? commandTimeout = null) => Conn.Delete(entityToDelete, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Deletes a record or records in the database by ID</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where the Id property and properties with the [Key] attribute match those in the database</para>
        /// <para>The number of records affected</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public int Delete(TKey id, int? commandTimeout = null) => Conn.Delete(id, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Deletes a record or records in the database that match the object passed in asynchronously</para>
        /// <para>-By default deletes records in the table matching the class name</para>
        /// <para>Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the number of records affected</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityToDelete"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public async Task<int> DeleteAsync(TEntity entityToDelete, int? commandTimeout = null) => await Conn.DeleteAsync(entityToDelete, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Deletes a record or records in the database by ID asynchronously</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where the Id property and properties with the [Key] attribute match those in the database</para>
        /// <para>The number of records affected</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public async Task<int> DeleteAsync(TKey id, int? commandTimeout = null) => await  Conn.DeleteAsync(id, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Deletes a list of records in the database</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where that match the where clause</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public int DeleteList(string conditions, object parameters = null, int? commandTimeout = null) => Conn.DeleteList<TEntity>(conditions, parameters, Context.Tran, commandTimeout);

        /// <summary>
        /// <para>Deletes a list of records in the database</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where that match the where clause</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age&gt;=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public async Task<int> DeleteListAsync(string conditions, object parameters = null, int? commandTimeout = null) => await Conn.DeleteListAsync<TEntity>(conditions, parameters, Context.Tran, commandTimeout);
    }
}