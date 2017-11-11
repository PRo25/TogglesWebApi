using Moq;
using System.Linq;

namespace Toggles.DataAccess.Mocks.Extensions
{
    public static class MockOfIQueryableExtensions
    {
        public static void SetupAsQueryable<TQuery>(this Mock<TQuery> mockOfQuery, IQueryable entities)
            where TQuery : class, IQueryable
        {
            Mock<IQueryable> mockOfIQueryable = mockOfQuery.As<IQueryable>();
            mockOfIQueryable.SetupWithQueryableEntities(entities);
        }

        private static void SetupWithQueryableEntities(this Mock<IQueryable> mockOfQuery, IQueryable entities)
        {
            mockOfQuery.Setup(m => m.Provider).Returns(entities.Provider);
            mockOfQuery.Setup(m => m.Expression).Returns(entities.Expression);
            mockOfQuery.Setup(m => m.ElementType).Returns(entities.ElementType);
            mockOfQuery.Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
        }
    }
}
