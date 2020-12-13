using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BusNotReadyException : Exception
    {
        public BusNotReadyException()
        {
        }

        public BusNotReadyException(string message) : base(message)
        {
        }

        public BusNotReadyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusNotReadyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}