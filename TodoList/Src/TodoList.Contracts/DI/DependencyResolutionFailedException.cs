using System;
using System.Runtime.Serialization;

namespace TodoList.Contracts.DI
{
    public class DependencyResolutionFailedException : Exception
    {
        public DependencyResolutionFailedException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        protected DependencyResolutionFailedException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}