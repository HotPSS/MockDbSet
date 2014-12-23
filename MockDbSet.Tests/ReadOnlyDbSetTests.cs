using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;

namespace MockDbSet.Tests
{
    [TestFixture]
    public class ReadOnlyDbSetTests : MockDbSetBaseTests
    {
        [Test]
        public void Count()
        {
            var data = new List<Person> { new Person() }; 
            var sut = new ReadOnlyDbSet<Person>(data.AsQueryable());

            Assert.That(sut.Count(), Is.EqualTo(1));
        }

        public override MockDbSetBase<TEntity> CreateSut<TEntity>(List<TEntity> data, Action<string, IEnumerable> include = null)
        {
            return new ReadOnlyDbSet<TEntity>(data.AsQueryable(), include);
        }
    }
}
