using System;
using System.Runtime.Serialization;

namespace FlightManagementSystem.DBConnection
{
    // HI ! :)


    [Serializable]
    internal class AirCompanyNotFoundException : Exception
    {
        public AirCompanyNotFoundException()
        {
        }

        public AirCompanyNotFoundException(string message) : base(message)
        {
        }

        public AirCompanyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirCompanyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}