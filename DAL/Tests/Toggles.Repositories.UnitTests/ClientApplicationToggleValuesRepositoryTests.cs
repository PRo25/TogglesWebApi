using NUnit.Framework;
using Toggles.BusinessEntities;

namespace Toggles.Repositories.UnitTests
{
    [TestFixture]
    public class ClientApplicationToggleValuesRepositoryTests
        : RepositoryTestsBase<ClientApplicationToggleValuesRepository, ClientApplicationToggleValue>
    {
        protected override ClientApplicationToggleValuesRepository CreateRepository()
        {
            var repository = new ClientApplicationToggleValuesRepository();
            return repository;
        }
    }
}
