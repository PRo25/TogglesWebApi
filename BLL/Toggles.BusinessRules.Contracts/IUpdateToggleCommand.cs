using Toggles.BusinessEntities;

namespace Toggles.BusinessRules.Contracts
{
    public interface IUpdateToggleCommand
    {
        void Execute(Toggle toggle);
    }
}