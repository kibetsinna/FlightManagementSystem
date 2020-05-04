using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public interface ILoggedInAdministratorFacade :IAnonymousUserFacade
    {
        int CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        int CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        int CreateNewCountry(LoginToken<Administrator> token, Country country);
        void UpdateCountryName(LoginToken<Administrator> token, Country country);
        void RemoveCountry(LoginToken<Administrator> token, Country country);


    }
}
