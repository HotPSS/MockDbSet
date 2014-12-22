// Copyright (c) Marcel Veldhuizen. All rights reserved.

using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace MockDbSet
{
    /// <summary>
    /// Replacement for <see cref="DbSet{TEntity}" /> to facilitate testing of Entity Framework dependent code.
    /// </summary>
    /// <typeparam name="TEntity">The type that defines the set.</typeparam>
    public class InMemoryDbSet<TEntity> : MockDbSetBase<TEntity> where TEntity: class
    {
        /// <summary>
        /// Creates an instance of a <see cref="InMemoryDbSet{TEntity}" />.
        /// </summary>
        /// <param name="queryable">Source of data.</param>
        /// <param name="include"></param>
        public InMemoryDbSet(IQueryable<TEntity> queryable, Action<string, IEnumerable> include = null) : base(queryable, include)
        {
        }

        public override DbSet<TEntity> CreateChildSet(IQueryable<TEntity> queryable)
        {
            return new InMemoryDbSet<TEntity>(queryable);
        }
    }
}
