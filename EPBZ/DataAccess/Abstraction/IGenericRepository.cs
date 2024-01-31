using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstraction
{
    public interface IGenericRepository<TEntity>
    {
        IDbConnection GetOpenConnection();
        TEntity GetSingle(int aId);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllById(int aId);
        void Insert(TEntity aEntity);
        void Update(TEntity aEntity);
        void Delete(int aId);
    }
}
