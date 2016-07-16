using System;
using System.Linq;
using System.Linq.Expressions;

namespace HchWebPhr.Data.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        int Create(TEntity instance);

        int Update(TEntity instance);

        int Delete(TEntity instance);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        int SaveChanges();
    }
}
