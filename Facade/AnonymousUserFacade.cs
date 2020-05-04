using FlightManagementSystem.DBConnection;
using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        //IList<AirlineCompany> IAnonymousUserFacade.GetAllAirlineCompanies()
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            return _airlineDAO1.GetAll();
        }

        //IList<Flight> IAnonymousUserFacade.GetAllFlights()
        public IList<Flight> GetAllFlights()
        {

            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetAll();
        }

        //IList<Flight> IAnonymousUserFacade.GetAllFlightsByAirLineCompanies(AirlineCompany comp)
        public IList<Flight> GetAllFlightsByAirLineCompanies(AirlineCompany comp)
        {

            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByAirlineCompany(comp);
        }

        //Dictionary<Flight, int> IAnonymousUserFacade.GetAllFlightsVacancy()
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetAllFlightsVacancy();
        }

        //Flight IAnonymousUserFacade.GetFlightById(int id)
        public Flight GetFlightById(int id)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.Get(id);
        }

       // IList<Flight> IAnonymousUserFacade.GetFlightsByDepartureDate(DateTime departureDate)
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByDepartureTime(departureDate);

        }

        //IList<Flight> IAnonymousUserFacade.GetFlightsByDestinationCountry(int countryCode)
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByDestinationCountry(countryCode);
        }

       // IList<Flight> IAnonymousUserFacade.GetFlightsByLandingDate(DateTime landingDate)
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByLandingTime(landingDate);
        }

        //IList<Flight> IAnonymousUserFacade.GetFlightsByOriginCountry(int countryCode)
        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByOriginCountry(countryCode);
        }
    }
}
