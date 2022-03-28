using System;
using System.Data;
using  Dapper;

namespace Comman.Dapper
{
    public class Class1
    {
        private IDbConnection _connection;

        public Class1(IDbConnection connection)
        {
            _connection = connection;

        }

        public void GetData()
        {
        
        }
    }
}
