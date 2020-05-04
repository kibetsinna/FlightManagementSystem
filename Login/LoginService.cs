using FlightManagementSystem.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.PocoLogin
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            bool result = false;
            token = null;
            if ( userName == "admin"&& password == "9999")
            {
                token = new LoginToken<Administrator>();
                token.User = new Administrator();
                result = true;
            }
            return result;
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            bool result = false;
            token = null;
            AirlineCompany airline = null;
           
            _airlineDAO = new AirlineDAOMSSQL();
            AirlineDAOMSSQL _airlineDAO1 = (AirlineDAOMSSQL)_airlineDAO;
            airline = _airlineDAO1.GetAirlineByUserName(userName);
            if (!(airline is null))
            {
                if (airline.Password != password)
                {
                    throw new WrongPasswordException("This password is wrong");
                }
                else
                {
                    token = new LoginToken<AirlineCompany>();
                    token.User = airline;
                    result = true;
                }
            }
            return result;

        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            bool result = false;
            token = null;
            Customer customer = null;

            _customerDAO = new CustomerDAOMSSQL();
            CustomerDAOMSSQL _customerDAO1 = (CustomerDAOMSSQL) _customerDAO;
            customer = _customerDAO1.GetCustomerByUserName(userName);
            if (!(customer is null))
            {
                if (customer.Password != password)
                {
                    throw new WrongPasswordException("This password is wrong");
                }
                else
                {
                    token = new LoginToken<Customer>();
                    token.User = customer;
                    result = true;
                }
            }
            return result;
        }
        public bool TryLogin(string userName,string password, out ILoginToken token)
        {
            bool result = false;
            LoginToken<Administrator> tokenA = null;
            result = TryAdminLogin(userName, password, out tokenA);
            if (result == true)
                 token = tokenA;
            else
            {
                LoginToken<AirlineCompany> tokenAC = null;
                result = TryAirlineLogin(userName, password, out tokenAC);
                if (result == true)
                    token = tokenAC;
                else 
                {
                    LoginToken<Customer> tokenC = null;
                    result = TryCustomerLogin(password, userName, out tokenC);
                    if (result == true)
                        token = tokenC;
                    else
                        throw new UserNotFoundException("User Not Found");
                }
              
            }
          
            return result;
        }
    }
}
