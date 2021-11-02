using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Common;
using System.Data;

namespace PlanningPoker.Repository.Class
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDBContext RepositoryContext { get; set; }
        internal DbSet<T> DbSet;

        public GenericRepository(AppDBContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
            DbSet = repositoryContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await this.RepositoryContext.Set<T>().FindAsync(id);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => this.RepositoryContext.Set<T>().FirstOrDefaultAsync(predicate);

        public void Insert(T entity)
        {
            this.RepositoryContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.RepositoryContext.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAllAsync() => this.RepositoryContext.Set<T>().CountAsync();

        public Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
            => this.RepositoryContext.Set<T>().CountAsync(predicate);


        public List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var command = RepositoryContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                RepositoryContext.Database.OpenConnection();

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
    }
}
