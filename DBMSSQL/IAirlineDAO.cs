using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.DBConnection
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserName(string name);
        List<AirlineCompany> GetAllAirlinesCompanyByCountry(int countryId);
    }
}
