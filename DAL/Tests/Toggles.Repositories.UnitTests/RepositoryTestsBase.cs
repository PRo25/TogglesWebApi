using NUnit.Framework;
using System;

namespace Toggles.Repositories.UnitTests
{
    public abstract class RepositoryTestsBase<TRepository, TBusinessEntity>
        where TRepository : RepositoryBase<TBusinessEntity>
        where TBusinessEntity : class, new()
    {
        [Test]
        public void TestAdd()
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();

            TestDelegate action = () => repository.Add(businessEntity);

            Assert.DoesNotThrow(action);
            Assert.IsTrue(repository.Added.Contains(businessEntity));
        }

        protected abstract TRepository CreateRepository();

        protected virtual TBusinessEntity CreateMockedBusinessEntity()
        {
            return new TBusinessEntity();
        }

        [Test]
        public void TestAdd_FailsWhenEntityIsUpdated()
        {
            this.TestAddFailsWhenBefore(
                (repository, businessEntity) => repository.Update(businessEntity));
        }

        private void TestAddFailsWhenBefore(
            Action<TRepository, TBusinessEntity> changeRepositoryToInvalidStateAction)
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();
            changeRepositoryToInvalidStateAction(repository, businessEntity);

            TestDelegate action = () => repository.Add(businessEntity);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void TestAdd_FailsWhenEntityIsDeleted()
        {
            this.TestAddFailsWhenBefore(
                (repository, businessEntity) => repository.Delete(businessEntity));
        }

        [Test]
        public void TestUpdate()
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();

            TestDelegate action = () => repository.Update(businessEntity);

            Assert.DoesNotThrow(action);
            Assert.IsTrue(repository.Updated.Contains(businessEntity));
        }

        [Test]
        public void TestUpdate_FailsWhenEntityIsAdded()
        {
            this.TestUpdateFailsWhenBefore(
                (repository, businessEntity) => repository.Add(businessEntity));
        }

        private void TestUpdateFailsWhenBefore(
            Action<TRepository, TBusinessEntity> changeRepositoryToInvalidStateAction)
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();
            changeRepositoryToInvalidStateAction(repository, businessEntity);

            TestDelegate action = () => repository.Update(businessEntity);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Test]
        public void TestUpdate_FailsWhenEntityIsDeleted()
        {
            this.TestUpdateFailsWhenBefore(
                (repository, businessEntity) => repository.Delete(businessEntity));
        }

        [Test]
        public void TestDelete()
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();

            TestDelegate action = () => repository.Delete(businessEntity);

            Assert.DoesNotThrow(action);
            Assert.IsTrue(repository.Deleted.Contains(businessEntity));
        }

        [Test]
        public void TestDelete_WhenEntityIsAdded()
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();
            repository.Add(businessEntity);

            TestDelegate action = () => repository.Delete(businessEntity);

            Assert.DoesNotThrow(action);
            Assert.IsFalse(repository.Added.Contains(businessEntity));
            Assert.IsFalse(repository.Deleted.Contains(businessEntity));
        }

        [Test]
        public void TestDelete_WhenEntityIsUpdated()
        {
            TRepository repository = this.CreateRepository();
            TBusinessEntity businessEntity = this.CreateMockedBusinessEntity();
            repository.Update(businessEntity);

            TestDelegate action = () => repository.Delete(businessEntity);

            Assert.DoesNotThrow(action);
            Assert.IsFalse(repository.Updated.Contains(businessEntity));
            Assert.IsTrue(repository.Deleted.Contains(businessEntity));
        }
    }
}
