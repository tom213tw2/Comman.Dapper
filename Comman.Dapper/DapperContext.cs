using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Comman.Dapper
{
    public class DapperContext:IRepositoryContext,IDisposable,IUnitOfWork
    {
        private readonly ConnectionStringSettings _connectionSeting;
        private IDbConnection _Conn;
        private bool _committed = true;
        private readonly object _sync = new object();

        public DapperContext()
        {
            string appSetting = ConfigurationManager.AppSettings["DefaultConn"];
            this._connectionSeting = (!string.IsNullOrEmpty(appSetting) ? ConfigurationManager.ConnectionStrings[appSetting] : throw new Exception()) ?? throw new Exception();
            this.InitConnection();
        }

        public DapperContext(ConnectionStringSettings connectionSeting)
        {
            this._connectionSeting = connectionSeting;
            this.InitConnection();
        }

        public DapperContext(string connectionString, string providerName)
        {
            this._connectionSeting = new ConnectionStringSettings("conn", connectionString, providerName);
            this.InitConnection();
        }

        public DapperContext(string MSSQLCnnectionString)
        {
            this._connectionSeting = new ConnectionStringSettings("conn", MSSQLCnnectionString, "System.Data.SqlClient");
            this.InitConnection();
        }

        public IDbConnection Conn
        {
            get
            {
                if (this._Conn == null)
                    this.InitConnection();
                return this._Conn;
            }
            private set => this._Conn = value;
        }

        public void InitConnection()
        {
            this.Conn = (IDbConnection)DbProviderFactories.GetFactory(this._connectionSeting.ProviderName).CreateConnection();
            if (this.Conn == null)
                return;
            this.Conn.ConnectionString = this._connectionSeting.ConnectionString;
        }

        public int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) => this.Conn.Execute(sql, param, this.Tran, commandTimeout, commandType);

        public IEnumerable<T> Query<T>(
          string sql,
          object param = null,
          int? commandTimeout = null,
          CommandType? commandType = null)
        {
            return this.Conn.Query<T>(sql, param, this.Tran, commandTimeout: commandTimeout, commandType: commandType);
        }

        public bool Committed
        {
            set => this._committed = value;
            get => this._committed;
        }

        public IDbTransaction Tran { private set; get; }

        public void BeginTran()
        {
            if (!this.Committed)
                throw new Exception();
            if (this.Conn.State == ConnectionState.Closed)
                this.Conn.Open();
            this.Tran = this.Conn.BeginTransaction();
            this.Committed = false;
        }

        public void Commit()
        {
            if (this.Committed)
                return;
            lock (this._sync)
            {
                this.Tran.Commit();
                this._committed = true;
            }
        }

        public void Rollback()
        {
            if (this.Committed)
                return;
            lock (this._sync)
            {
                this.Tran.Rollback();
                this._committed = true;
            }
        }

        public void Dispose()
        {
            if (this.Conn.State != ConnectionState.Open)
                return;
            if (!this.Committed)
                this.Rollback();
            this.Conn.Close();
            this.Conn.Dispose();
        }
    }
}
