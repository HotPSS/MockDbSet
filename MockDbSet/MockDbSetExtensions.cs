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
        /// Wraps the IQueryable in an <see cref="ReadOnlyDbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type that defines the set.</typeparam>
        /// <param name="queryable">Source of data for the <see cref="ReadOnlyDbSet{TEntity}" />.</param>
        /// <returns>A ReadOnlyDbSet.</returns>
        public static DbSet<TEntity> AsDbSet<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class
        {
            return new ReadOnlyDbSet<TEntity>(queryable);
        }

        /// <summary>
        /// Wraps the IEnumerable in an <see cref="ReadOnlyDbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type that defines the set.</typeparam>
        /// <param name="enumerable">Source of data for the <see cref="ReadOnlyDbSet{TEntity}" />.</param>
        /// <returns>A ReadOnlyDbSet.</returns>
        public static DbSet<TEntity> AsDbSet<TEntity>(this IEnumerable<TEntity> enumerable) where TEntity : class
        {
            return new ReadOnlyDbSet<TEntity>(enumerable.AsQueryable());
        }

        /// <summary>
        /// Wraps the IList in a <see cref="FakeDbSet{TEntity}" />.
        /// </summary>
        /// <typeparam name="TEntity">The type that defines the set.</typeparam>
        /// <param name="list">Source of data for the <see cref="FakeDbSet{TEntity}" />.</param>
        /// <returns>A FakeDbSet.</returns>
        public static FakeDbSet<TEntity> AsDbSet<TEntity>(this IList<TEntity> list) where TEntity : class
        {
            return new FakeDbSet<TEntity>(list);
        }
    }
}
