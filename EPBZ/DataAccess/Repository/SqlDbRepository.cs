using DataAccess.Abstraction;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repository
{
    public abstract class SqlDbRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private string _connectionString;

        public SqlDbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public abstract IEnumerable<TEntity> GetAll();
        public abstract IEnumerable<TEntity> GetAllById(int aId);
        public abstract TEntity GetSingle(int aId);
        public abstract void Insert(TEntity aEntityToUpdate);
        public abstract void Update(TEntity aEntityToUpdate);
        public abstract void Delete(int aId);
    }
}
