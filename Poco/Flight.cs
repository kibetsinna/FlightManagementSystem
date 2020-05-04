using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.PocoLogin
{
    public class Flight : IPoco, IUser
   

    {
        private int _id;
        private int _airlineCompanyId;
        private int _originCountryCode;
        private int _destinationCountryCode;
        private DateTime _departureTime;
        private DateTime _landingTime;
        private int _remainingTickets;
        private int _flightStatus;
        public int Id
        {

            get { return _id; }
            set { _id = value; }

        }
        public int AirlineCompanyId
        {

            get { return _airlineCompanyId; }
            set { _airlineCompanyId = value; }

        }
        public int OriginCountryCode
        {

            get { return _originCountryCode; }
            set { _originCountryCode = value; }

        }
        public int DestinationCountryCode
        {

            get { return _destinationCountryCode; }
            set { _destinationCountryCode = value; }

        }
        public DateTime DepartureTime
        {

            get { return _departureTime; }
            set { _departureTime = value; }

        }
        public DateTime LandingTime
        {

            get { return _landingTime; }
            set { _landingTime = value; }

        }
        public int RemainingTickets
        {

            get { return _remainingTickets; }
            set { _remainingTickets = value; }

        }
        public int FlightStatus
        {

            get { return _flightStatus; }
            set { _flightStatus = value; }

        }
        public Flight(int airlineCompanyId, int originCountryCode, int destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets, int flightStatus)
        {
            _airlineCompanyId = airlineCompanyId;
            _originCountryCode = originCountryCode;
            _destinationCountryCode = destinationCountryCode;
            _departureTime = departureTime;
            _landingTime = landingTime;
            _remainingTickets = remainingTickets;
            _flightStatus = flightStatus;
        }

        public Flight() { }

      

        public override bool Equals(object obj)
        {
            return obj is Flight f &&
                   Id == f.Id;
        }



        public override string ToString()
        {
            return ($"Flight {Id} ,{AirlineCompanyId}, {OriginCountryCode}, {DestinationCountryCode}, {DepartureTime}, {LandingTime}, {RemainingTickets}, {FlightStatus}");
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(Flight f1, Flight f2)
        {
            return (f1.Id == f2.Id);
        }
        public static bool operator !=(Flight f1, Flight f2)
        {
            return !(f1 == f2);
        }
    }
}
