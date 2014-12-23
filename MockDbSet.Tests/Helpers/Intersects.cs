using System.Collections;
using System.Linq;
using NUnit.Framework.Constraints;

namespace MockDbSet.Tests.Helpers
{
    internal class Intersects : CollectionConstraint
    {
        private readonly IEnumerable _otherCollection;

        public Intersects(IEnumerable otherCollection) : base(otherCollection)
        {
            _otherCollection = otherCollection;
        }

        public new static Intersects With(IEnumerable arg)
        {
            return new Intersects(arg);
        }

        protected override bool doMatch(IEnumerable collection)
        {
            return _otherCollection.Cast<object>().Intersect(collection.Cast<object>()).Any();
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("intersecting collections");
        }
    }
}
