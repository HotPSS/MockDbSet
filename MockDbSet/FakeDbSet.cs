// Copyright (c) Marcel Veldhuizen. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockDbSet
{
    /// <summary>
    /// Replacement for <see cref="DbSet{TEntity}" /> to facilitate testing of Entity Framework dependent code.
    /// </summary>
    /// <typeparam name="TEntity">The type that defines the set.</typeparam>
    public class FakeDbSet<TEntity> : MockDbSetBase<TEntity> where TEntity: class
    {
        private readonly IList<TEntity> _data;

        /// <summary>
        /// Creates an instance of a <see cref="ReadOnlyDbSet{TEntity}" />.
        /// </summary>
        /// <param name="data">Source of data. Must not be read only, or an array.</param>
        /// <param name="include"></param>
        public FakeDbSet(IList<TEntity> data, Action<string, IEnumerable> include = null) : base(data.AsQueryable(), include)
        {
            if (data is TEntity[])
            {
                throw new ArgumentException("Unable to use array for data since it does not support adding or removing items. Use List<T> or use InMemoryDbSet which does not support modification.", "data");
            }

            if (data.IsReadOnly)
            {
                throw new ArgumentException("Unable to use read-only list for data. Use List<T> or use InMemoryDbSet which does not support modification.", "data");
            }

            _data = data;
        }

        /// <inheritdoc />
        public override TEntity Add(TEntity entity)
        {
            _data.Add(entity);
            return entity;
        }

        /// <inheritdoc />
        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var items = entities.ToList().AsReadOnly();
            
            foreach (var item in items)
            {
                _data.Add(item);
            }

            return items;
        }

        /// <inheritdoc />
        public override TEntity Remove(TEntity entity)
        {
            _data.Remove(entity);
            return entity;
        }

        /// <inheritdoc />
        public override IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            var items = entities.ToList().AsReadOnly();

            foreach (var item in items)
            {
                _data.Remove(item);
            }

            return items;            
        }

        /// <inheritdoc />
        protected override DbSet<TEntity> CreateChildSet(IQueryable<TEntity> queryable)
        {
            return new FakeDbSet<TEntity>(queryable.ToList());
        }
    }
}
