using Dapper;
using System;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);

        void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);

        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null);
        T QuerySingle<T>(string procedureName, DynamicParameters param = null);
    }
}
