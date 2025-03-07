using System;

namespace Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message) : base(message) { }
        public EntityAlreadyExistsException(string entityName, string id) : base($"{entityName ?? "Record"} with Id:{id} already exists.") { }
    }
}
