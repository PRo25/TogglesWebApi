using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts
{
    public interface ICreateToggleCommand
    {
        void Execute(Toggle toggle);
    }
}