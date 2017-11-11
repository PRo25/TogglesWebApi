using Moq;
using System.Collections.Generic;

namespace Toggles.Repositories.Contracts.Mocks
{
    public abstract class RepositoryMock<TRepository, TBusinessEntity> : Mock<TRepository>
        where TRepository : class, IRepository<TBusinessEntity>
        where TBusinessEntity : class
    {
        private IList<TBusinessEntity> added = new List<TBusinessEntity>();
        private IList<TBusinessEntity> updated = new List<TBusinessEntity>();
        private IList<TBusinessEntity> deleted = new List<TBusinessEntity>();

        public int NumberOfChanges { get; private set; }

        public RepositoryMock()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.SetupGet(m => m.Added).Returns(() => this.added);
            this.SetupGet(m => m.Updated).Returns(() => this.updated);
            this.SetupGet(m => m.Deleted).Returns(() => this.deleted);
            this.SetupAdd();
            this.SetupUpdate();
            this.SetupDelete();
        }

        private void SetupAdd()
        {
            this.Setup(m => m.Add(It.IsAny<TBusinessEntity>()))
                .Callback((TBusinessEntity entity) =>
                {
                    this.added.Add(entity);
                    this.NumberOfChanges++;
                });
        }

        private void SetupUpdate()
        {
            this.Setup(m => m.Update(It.IsAny<TBusinessEntity>()))
                .Callback((TBusinessEntity entity) =>
                {
                    this.updated.Add(entity);
                    this.NumberOfChanges++;
                });
        }

        private void SetupDelete()
        {
            this.Setup(m => m.Delete(It.IsAny<TBusinessEntity>()))
                .Callback((TBusinessEntity entity) =>
                {
                    this.deleted.Add(entity);
                    this.NumberOfChanges++;
                });
        }
    }
}
