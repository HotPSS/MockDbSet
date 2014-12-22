using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MockDbSet.Tests
{
    [TestFixture]
    public abstract class MockDbSetBaseTests
    {
        public abstract MockDbSetBase<TEntity> CreateSut<TEntity>(List<TEntity> data, Action<string, IEnumerable> include = null) where TEntity : class;

        [Test]
        public void Include_WithExpression_ShouldCallIncludeAction()
        {
            var includedPaths = new List<string>();

            var data = new List<Person> { new Person() };
            DbSet<Person> sut = CreateSut(data, (path, enumerable) => includedPaths.Add(path));
            sut.Include(p => p.HomeAddress);

            Assert.That(includedPaths, Contains.Item("HomeAddress"));
        }

        [Test]
        public async Task ToListAsync()
        {
            var cts = new CancellationTokenSource();
            var data = new List<Person> { new Person() };
            DbSet<Person> sut = CreateSut(data);

            var result = await sut.ToListAsync(cts.Token);

            Assert.That(result, Is.EquivalentTo(data));
        }
    }
}
