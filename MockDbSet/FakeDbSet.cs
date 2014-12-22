using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockDbSet
{
    public class FakeDbSet<TEntity> : MockDbSetBase<TEntity> where TEntity: class
    {
        private IList<TEntity> _data;

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

        public override TEntity Add(TEntity entity)
        {
            _data.Add(entity);
            return entity;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var items = entities.ToList().AsReadOnly();
            
            foreach (var item in items)
            {
                _data.Add(item);
            }

            return items;
        }

        public override TEntity Remove(TEntity entity)
        {
            _data.Remove(entity);
            return entity;
        }

        public override IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            var items = entities.ToList().AsReadOnly();

            foreach (var item in items)
            {
                _data.Remove(item);
            }

            return items;            
        }

        public override DbSet<TEntity> CreateChildSet(IQueryable<TEntity> queryable)
        {
            return new FakeDbSet<TEntity>(queryable.ToList());
        }
    }
}
