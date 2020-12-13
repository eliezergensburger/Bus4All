using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BlBusException : Exception
    {
        public BlBusException()
        {
        }

        public BlBusException(string message) : base(message)
        {
        }

        public BlBusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlBusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}