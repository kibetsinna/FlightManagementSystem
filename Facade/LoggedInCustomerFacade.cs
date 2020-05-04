using FlightManagementSystem.DBConnection;
using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        //void ILoggedInCustomerFacade.CancelTicket(LoginToken<Customer> token, Ticket ticket)
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL)_ticketDAO;
            _ticketDAO1.Remove(ticket);
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            Flight f = _flightDAO1.Get(ticket.FlightId);
            f.RemainingTickets = f.RemainingTickets+1;
            _flightDAO1.Update(f);
        }

        //IList<Flight> ILoggedInCustomerFacade.GetAllMyFlights(LoginToken<Customer> token)
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            FlightDAOMSSQL _flightDAO1 = (FlightDAOMSSQL)_flightDAO;
            return _flightDAO1.GetFlightsByCustomer(token.User);
        }

        //Ticket ILoggedInCustomerFacade.PurchaseTicket(LoginToken<Customer> token, Flight flight)
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)

        {
            TicketDAOMSSQL _ticketDAO1 = (TicketDAOMSSQL)_ticketDAO;
            Ticket ticket = new Ticket(flight.Id, token.User.Id);
            int id = _ticketDAO1.Add(ticket);
            flight.RemainingTickets = flight.RemainingTickets - 1;
            _flightDAO.Update(flight);
            ticket.Id = id;
            return ticket;
        }
    }
}
