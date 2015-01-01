namespace cleancoderscom
{
    using NUnit.Framework;

    public class EntityTest
    {
        [Test]
        public virtual void twoDifferentEntitysAreNotTheSame()
        {
            Entity e1 = new Entity();
            Entity e2 = new Entity();

            e1.Id = "e1ID";
            e2.Id = "e2ID";

            Assert.IsFalse(e1.isSame(e2));
        }

        [Test]
        public virtual void oneEntityIsTheSameAsItself()
        {
            Entity e1 = new Entity();
            e1.Id = "e1ID";

            Assert.IsTrue(e1.isSame(e1));
        }

        [Test]
        public virtual void EntitysWithTheSameIdAreTheSame()
        {
            Entity e1 = new Entity();
            Entity e2 = new Entity();
            e1.Id = "e1ID";
            e2.Id = "e1ID";

            Assert.IsTrue(e1.isSame(e2));
        }

        [Test]
        public virtual void EntitysWithNullIdsAreNeverSame()
        {
            Entity e1 = new Entity();
            Entity e2 = new Entity();

            Assert.IsFalse(e1.isSame(e2));
        }
    }

}