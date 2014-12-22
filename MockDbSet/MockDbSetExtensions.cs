// Copyright (c) Marcel Veldhuizen. All rights reserved.

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockDbSet
{
    /// <summary>
    /// Extension methods for convenience.
    /// </summary>
    public static class MockDbSetExtensions
    {
        /// <summary>
        /// Wraps the IQueryable in an <see cref="InMemoryDbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type that defines the set.</typeparam>
        /// <param name="queryable">Source of data for the <see cref="InMemoryDbSet{TEntity}" />.</param>
        /// <returns>An InMemoryDbSet.</returns>
        public static DbSet<TEntity> AsDbSet<TEntity>(this IQueryable<TEntity> queryable) where TEntity: class
        {
            return new InMemoryDbSet<TEntity>(queryable);
        }

        /// <summary>
        /// Wraps the IEnumerable in an <see cref="InMemoryDbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type that defines the set.</typeparam>
        /// <param name="enumerable">Source of data for the InMemoryDbSet.</param>
        /// <returns>An InMemoryDbSet.</returns>
        public static DbSet<TEntity> AsDbSet<TEntity>(this IEnumerable<TEntity> enumerable) where TEntity : class
        {
            return new InMemoryDbSet<TEntity>(enumerable.AsQueryable());
        }
    }
}
