using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Domain.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace PlanningPoker.Repository.Class
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDBContext _repoContext;

        private IDbContextTransaction _transaction;


        public UnitOfWork(AppDBContext repositoryContext)
        {
            _repoContext = repositoryContext;

        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        public void SaveAsync()
        {
            _repoContext.SaveChangesAsync();

        }

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        public void StartTransaction()
        {
            _transaction = _repoContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        /// <param name="level">The level.</param>
        public void StartTransaction(IsolationLevel level)
        {
            _transaction = _repoContext.Database.BeginTransaction(level);

        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        public void CommitAsync()
        {
            try
            {
                _repoContext.SaveChangesAsync();
                _transaction.CommitAsync();
            }
            catch (Exception)
            {
                _transaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Rollbacks the asynchronous.
        /// </summary>
        public void RollbackAsync()
        {
            _transaction.RollbackAsync();
        }

        public List<T> SqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var command = _repoContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                _repoContext.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }

        }
        public List<T> SqlProcedure<T>(string spName, Func<DbDataReader, T> map, SqlParameter[] parameters = null)
        {
            using (var command = _repoContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = spName;
                command.CommandType = CommandType.StoredProcedure;
                _repoContext.Database.OpenConnection();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }

        }

        public List<T> ExecuteReader<T>(string commandText, CommandType commandType, Func<DbDataReader, T> map, SqlParameter[] parameters = null)
        {
            using (var command = _repoContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.Text;
                _repoContext.Database.OpenConnection();
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    return entities;
                }
            }
        }

        public AppDBContext AppDbContext
        {
            get { return _repoContext; }
        }

        #region Dispose

        private bool _disposed;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed || _repoContext != null)
            {
                _repoContext.Dispose();
            }
            _disposed = true;
        }

        #endregion Dispose


    }
}
