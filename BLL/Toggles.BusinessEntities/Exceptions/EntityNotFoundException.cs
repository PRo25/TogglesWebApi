using System;

namespace Toggles.BusinessEntities.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, string id)
            : base($"The entity of type '{entityType.Name}' and ID '{id}' was not found.")
        {

        }
    }
}
