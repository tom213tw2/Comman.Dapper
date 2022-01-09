using System.Data;

namespace Comman.Dapper
{
    public interface IUnitOfWork
    {
        bool Committed { get; set; }

        IDbTransaction Tran { get; }

        void BeginTran();

        void Commit();

        void Rollback();
    }
}