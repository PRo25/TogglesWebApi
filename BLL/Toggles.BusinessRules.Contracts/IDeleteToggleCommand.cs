using System;

namespace Toggles.BusinessRules.Contracts
{
    public interface IDeleteToggleCommand
    {
        void Execute(Guid toggleId);
    }
}