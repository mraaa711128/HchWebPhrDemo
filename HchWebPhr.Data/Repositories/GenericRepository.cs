using HchWebPhr.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace HchWebPhr.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context
        {
            get;
            set;
        }

        public GenericRepository()
            : this(new PhrEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Creates all specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int BulkInsert(List<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var entity in instance)
                {
                    this._context.Set<TEntity>().Add(entity);
                }
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Creates all specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int BulkUpdate(List<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var entity in instance)
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                }
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Creates all specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public int BulkDelete(List<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var entity in instance)
                {
                    this._context.Entry(entity).State = EntityState.Deleted;
                }
                return this.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                    new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    }))
            {
                return this._context.Set<TEntity>().FirstOrDefault(predicate);
            }
        }

        /// <summary>
        /// Get Entities by full fill the predicate
        /// </summary>
        /// <param name="predicate">The predicate to full fill.</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
        {
            using(var t = new TransactionScope(TransactionScopeOption.Suppress,
                  new TransactionOptions
                  {
                      IsolationLevel = IsolationLevel.ReadUncommitted
                  }))
            {
                return this._context.Set<TEntity>().Where(predicate).AsQueryable();
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                   new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   }))
            {
                return this._context.Set<TEntity>().AsQueryable();
            }
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
