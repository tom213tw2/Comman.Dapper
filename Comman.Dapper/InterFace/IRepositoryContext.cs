using System;
using System.Data;

namespace Comman.Dapper
{
    public interface IRepositoryContext : IDisposable, IUnitOfWork
    {
        IDbConnection Conn { get; }

        void InitConnection();
    }
}