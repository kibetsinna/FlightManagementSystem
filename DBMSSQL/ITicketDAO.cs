using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.DBConnection
{
    public interface ITicketDAO: IBasicDB<Ticket>
    {
    }
}
