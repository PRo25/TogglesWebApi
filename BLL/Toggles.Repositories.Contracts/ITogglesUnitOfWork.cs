namespace Toggles.Repositories.Contracts
{
    public interface ITogglesUnitOfWork
    {
        ITogglesRepository TogglesRepository { get; }
        IApplicationToggleValuesRepository ApplicationToggleValuesRepository { get; }
        int SaveChanges();
    }
}
