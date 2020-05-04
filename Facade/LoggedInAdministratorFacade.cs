using FlightManagementSystem.DBConnection;
using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        //int ILoggedInAdministratorFacade.CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        public int CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            return _airlineDAO1.Add(airline);
        }

        //     int ILoggedInAdministratorFacade.CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        public int CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            CustomerDAOMSSQL _customerDAO1 = (CustomerDAOMSSQL)_customerDAO;
            return _customerDAO1.Add(customer);
        }

        //void ILoggedInAdministratorFacade.RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            _airlineDAO1.Remove(airline);
        }

        //void ILoggedInAdministratorFacade.RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            CustomerDAOMSSQL _customerDAO1 = (CustomerDAOMSSQL)_customerDAO;
            _customerDAO1.Remove(customer);
        }

        //void ILoggedInAdministratorFacade.UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            _airlineDAO1.Update(airline);

        }

        //void ILoggedInAdministratorFacade.UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
       public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            CustomerDAOMSSQL _customerDAO1 = (CustomerDAOMSSQL)_customerDAO;
            _customerDAO1.Update(customer);
        }
        public int CreateNewCountry(LoginToken<Administrator> token, Country country)
        {

            CountryDAOMSSQL _countryDAO1 = (CountryDAOMSSQL)_countryDAO;
            return _countryDAO1.Add(country);
        }
        public void UpdateCountryName(LoginToken<Administrator> token, Country country)
        {
            CountryDAOMSSQL _countryDAO1 = (CountryDAOMSSQL)_countryDAO;
           _countryDAO1.Update(country);
        }
        public void RemoveCountry(LoginToken<Administrator> token, Country country)
        {
            CountryDAOMSSQL _countryDAO1 = (CountryDAOMSSQL)_countryDAO;
            _countryDAO1.Remove(country);
        }

        public void DeleteAll(LoginToken<Administrator> token)
        {
            if (token is null)
                return;
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            CustomerDAOMSSQL _customerDAO1 = (CustomerDAOMSSQL)_customerDAO;
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            CountryDAOMSSQL _countryDAO1 = (CountryDAOMSSQL)_countryDAO;
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL)_ticketDAO;
            _ticketDAO1.DeleteAll();
            _ticketDAO1.DeleteAllHistory();
            _flightDAO1.DeleteAll(); 
            _flightDAO1.DeleteAllHistory();
            _airlineDAO1.DeleteAll();
            _customerDAO1.DeleteAll();
            _countryDAO1.DeleteAll();
        }
        public void TransferFlightsToHistory(LoginToken<Administrator> token)
        {
            if (token is null)
                return;
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL)_ticketDAO;
            List<Flight> Flights = _flightDAO1.GetFlightsToTransfer();
            foreach (Flight f in Flights)
            {
                List<Ticket> tickets = _ticketDAO1.GetTicketsByFlight(f);
                foreach (Ticket t in tickets)
                {
                    _ticketDAO1.TransferTicketToHistory(t);
                }
                _flightDAO1.TransferFlightToHistory(f);
                _flightDAO1.Remove(f);
            }
            
          
        }
    }
}
