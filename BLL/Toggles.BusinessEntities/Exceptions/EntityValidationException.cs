using System;

namespace Toggles.BusinessEntities.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message)
            : base(message)
        {

        }
    }
}
