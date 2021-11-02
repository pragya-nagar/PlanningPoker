using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.SqlClient;
using PlanningPoker.Domain.Entities;

namespace PlanningPoker.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveAsync();
        void StartTransaction();
        void StartTransaction(IsolationLevel level);
        void CommitAsync();
        void RollbackAsync();
        List<T> SqlQuery<T>(string query, Func<DbDataReader, T> map);
        List<T> SqlProcedure<T>(string spName, Func<DbDataReader, T> map, SqlParameter[] parameters = null);
        List<T> ExecuteReader<T>(string commandText, CommandType commandType, Func<DbDataReader, T> map, SqlParameter[] parameters = null);
        AppDBContext AppDbContext { get; }

    }
}
