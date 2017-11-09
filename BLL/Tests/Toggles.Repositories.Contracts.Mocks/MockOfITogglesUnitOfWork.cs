using Moq;

namespace Toggles.Repositories.Contracts.Mocks
{
    public class MockOfITogglesUnitOfWork : Mock<ITogglesUnitOfWork>
    {
        public MockOfITogglesRepository MockOfITogglesRepository { get; private set; }

        public MockOfIClientApplicationToggleValuesRepository MockOfIClientApplicationToggleValuesRepository
        { get; private set; }

        public MockOfITogglesUnitOfWork()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.MockOfITogglesRepository = new MockOfITogglesRepository();
            this.MockOfIClientApplicationToggleValuesRepository = new MockOfIClientApplicationToggleValuesRepository();
            this.SetupTogglesRepository();
            this.SetupClientApplicationToggleValuesRepository();
        }

        private void SetupTogglesRepository()
        {
            this.Setup(m => m.TogglesRepository)
                .Returns(() => this.MockOfITogglesRepository.Object);
        }

        private void SetupClientApplicationToggleValuesRepository()
        {
            this.Setup(m => m.ClientApplicationToggleValuesRepository)
                .Returns(() => this.MockOfIClientApplicationToggleValuesRepository.Object);
        }
    }
}
