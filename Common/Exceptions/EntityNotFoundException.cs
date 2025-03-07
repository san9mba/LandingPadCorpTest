using System;

namespace Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string entityName, string id) : base($"{entityName ?? "Record"} with Id:{id} not found.") { }
    }
}
