using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MockDbSet.Tests.Helpers;
using NUnit.Framework;

namespace MockDbSet.Tests
{
    [TestFixture]
    public class FakeDbSetTests : MockDbSetBaseTests
    {
        [Test]
        public void Add_WithItem_ShouldAddItemToSourceList()
        {
            var data = new List<Person>();
            var sut = new FakeDbSet<Person>(data);

            var objToAdd = new Person();
            var result = sut.Add(objToAdd);

            Assert.That(data, Contains.Item(objToAdd));
            Assert.That(result, Is.SameAs(objToAdd));
        }

        [Test]
        public void AddRange_WithItems_ShouldAddItemsToSourceList()
        {
            var data = new List<Person>();
            var sut = new FakeDbSet<Person>(data);

            var itemsToAdd = new[] { new Person(), new Person() };
            var result = sut.AddRange(itemsToAdd);

            Assert.That(itemsToAdd,  Is.SubsetOf(data));
            Assert.That(result, Is.EquivalentTo(itemsToAdd));
        }

        [Test]
        public void RemoveRange()
        {
            var data = new List<Person> { new Person(), new Person(), new Person() };
            var itemsToRemove = data.Take(2).ToList();

            var sut = new FakeDbSet<Person>(data);

            var result = sut.RemoveRange(itemsToRemove);

            Assert.That(data, !Intersects.With(itemsToRemove));
            Assert.That(result, Is.EquivalentTo(itemsToRemove));
        }

        [Test]
        public void Remove_WithItemThatIsNotInSet_ShouldSucceed()
        {
            var objToRemove = new Person();
            var data = new List<Person>();
            var sut = new FakeDbSet<Person>(data);

            var result = sut.Remove(objToRemove);

            Assert.That(result, Is.SameAs(objToRemove));
        }

        [Test]
        public void Remove_WithItemThatIsInSet_ShouldRemoveItem()
        {
            var objToRemove = new Person();
            var data = new List<Person> { objToRemove };
            var sut = new FakeDbSet<Person>(data);

            var result = sut.Remove(objToRemove);

            Assert.That(result, Is.SameAs(objToRemove));
            Assert.That(data, !Contains.Item(objToRemove));
        }

        public override MockDbSetBase<TEntity> CreateSut<TEntity>(List<TEntity> data, Action<string, IEnumerable> include = null)
        {
            return new FakeDbSet<TEntity>(data, include);
        }
    }
}
