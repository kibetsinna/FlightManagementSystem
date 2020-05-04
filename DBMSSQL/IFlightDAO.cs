using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.DBConnection
{
    public interface IFlightDAO : IBasicDB<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsVacancy();
        // Flight GetFlightById(int id);=> Get(id)
       
        List<Flight> GetFlightsByOriginCountry(int countryCode);
        List<Flight> GetFlightsByDestinationCountry(int countryCode);
       
        List<Flight> GetFlightsByDepartureTime(DateTime departureDate);
       
        List<Flight> GetFlightsByLandingTime(DateTime landingDate);

        List<Flight> GetFlightsByCustomer(Customer customer);

    }
}
