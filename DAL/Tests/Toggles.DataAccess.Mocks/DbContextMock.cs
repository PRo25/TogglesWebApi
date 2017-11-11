using Common.Generic.Collections;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Toggles.DataAccess.Mocks.Extensions;

namespace Toggles.DataAccess.Mocks
{
    public abstract class DbContextMock<TDbContext> : Mock<TDbContext>
        where TDbContext : DbContext
    {
        private int nrOfChanges = 0;

        protected void Initialize()
        {
            this.CreateAndSetupMockedDbSets();
            this.SetupDbContext();
        }

        protected abstract void CreateAndSetupMockedDbSets();

        protected void CreateAndSetupMockedDbSetOf<TDbEntity>(IQueryable<TDbEntity> dbEntities,
            Expression<Func<TDbContext, DbSet<TDbEntity>>> dbSetSelectorExpression)
            where TDbEntity : class
        {
            dbEntities = dbEntities.GetSelfOrNewIfNull();
            DbSet<TDbEntity> mockedDbSet = this.CreateMockedDbSet(dbEntities);
            this.Setup(dbSetSelectorExpression).Returns(mockedDbSet);
        }

        private DbSet<T> CreateMockedDbSet<T>(IQueryable<T> entities)
            where T : class
        {
            var mockOfDbSet = new Mock<DbSet<T>>();
            mockOfDbSet.SetupAsQueryable(entities);
            this.SetupCallbacksToUpdateNrOfChanges(mockOfDbSet);
            mockOfDbSet.Setup(m => m.AsNoTracking()).Returns(mockOfDbSet.Object);
            return mockOfDbSet.Object;
        }

        private void SetupCallbacksToUpdateNrOfChanges<T>(Mock<DbSet<T>> mockOfDbSet)
            where T : class
        {
            mockOfDbSet.Setup(m => m.Add(It.IsAny<T>())).Callback(this.IncrementNrOfChanges);
            mockOfDbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback(this.IncrementNrOfChanges);
        }

        private void IncrementNrOfChanges()
        {
            this.nrOfChanges++;
        }

        private void SetupDbContext()
        {
            this.Setup(m => m.SaveChanges()).Returns(
                () =>
                {
                    return this.nrOfChanges;
                });
        }
    }
}
