using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.DBConnection
{
    [Serializable]
    internal class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException()
        {
        }

        public CustomerAlreadyExistsException(string message) : base(message)
        {
        }

        public CustomerAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}