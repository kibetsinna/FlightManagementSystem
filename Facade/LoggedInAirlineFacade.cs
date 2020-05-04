using FlightManagementSystem.DBConnection;
using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        //void ILoggedInAirlineFacade.CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL) _ticketDAO;
            List<Ticket> tickets = _ticketDAO1.GetTicketsByFlight(flight);
            foreach (Ticket t in tickets)
            {
                _ticketDAO1.Remove(t);
            }
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            _flightDAO1.Remove(flight);
        }

        //void ILoggedInAirlineFacade.ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            token.User.Password = newPassword;
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            _airlineDAO1.Update(token.User);
        }

        //int ILoggedInAirlineFacade.CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        public int CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL) _flightDAO;
            return _flightDAO1.Add(flight);
        }

        //IList<Flight> ILoggedInAirlineFacade.GetAllFlights(LoginToken<AirlineCompany> token)
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByAirlineCompany(token.User);
        }

        //IList<Ticket> ILoggedInAirlineFacade.GetAllTickets(LoginToken<AirlineCompany> token)
       public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL)_ticketDAO;
            return _ticketDAO1.GetTicketsByAirlineCompany(token.User);
        }

        //void ILoggedInAirlineFacade.ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL) _airlineDAO;
            _airlineDAO1.Update(airline);
        }

     //   void ILoggedInAirlineFacade.UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
       public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            _flightDAO1.Update(flight);
        }
    }
}
