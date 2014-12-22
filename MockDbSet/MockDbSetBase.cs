using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace MockDbSet
{
    /// <summary>
    /// Replacement for <see cref="DbSet" /> to facilitate testing of Entity Framework dependent code.
    /// </summary>
    /// <typeparam name="TEntity">The type that defines the set.</typeparam>
    public abstract class MockDbSetBase<TEntity> : DbSet<TEntity>, IQueryable<TEntity>, IDbAsyncEnumerable<TEntity> where TEntity : class
    {
        private readonly IQueryable<TEntity> _queryable;

        /// <summary>
        /// Creates an instance of a <see cref="InMemoryDbSet{TEntity}" />.
        /// </summary>
        /// <param name="queryable">Source of data.</param>
        /// <param name="include"></param>
        protected MockDbSetBase(IQueryable<TEntity> queryable, Action<string, IEnumerable> include = null)
        {
            _queryable = new InMemoryAsyncQueryable<TEntity>(queryable, include);
        }

        /// <inheritdoc />
        public override DbQuery<TEntity> Include(string path)
        {
            return CreateChildSet(_queryable.Include(path));
        }

        /// <inheritdoc />
        public virtual IQueryProvider Provider
        {
            get { return _queryable.Provider; }
        }

        /// <inheritdoc />
        public virtual Expression Expression
        {
            get { return _queryable.Expression; }
        }

        /// <inheritdoc />
        public virtual Type ElementType
        {
            get { return _queryable.ElementType; }
        }

        /// <inheritdoc />
        public virtual IEnumerator<TEntity> GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        /// <inheritdoc />
        public virtual IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
        {
            return ((IDbAsyncEnumerable<TEntity>)_queryable).GetAsyncEnumerator();
        }

        public abstract DbSet<TEntity> CreateChildSet(IQueryable<TEntity> queryable);
    }
}
