namespace Toggles.Repositories.Contracts
{
    public interface ITogglesUnitOfWork
    {
        ITogglesRepository TogglesRepository { get; }
        IClientApplicationToggleValuesRepository ClientApplicationToggleValuesRepository { get; }
        int SaveChanges();
    }
}
