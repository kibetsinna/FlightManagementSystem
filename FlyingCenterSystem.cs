using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightManagementSystem.Facade
{
    public class FlyingCenterSystem
    {
        AnonymousUserFacade _anonymousFacade = null;
        LoggedInAdministratorFacade _administratorFacade = null;
        LoggedInAirlineFacade _airlineFacade = null;
        LoggedInCustomerFacade _customerFacade = null;
        private Thread _thread;
        private bool _end = false;


        private int _threadTime = SQLConnection.threadTime;
        private static readonly FlyingCenterSystem _instance = new FlyingCenterSystem();
        
        static FlyingCenterSystem()
        {
        }

        private FlyingCenterSystem()
        {

            _thread = new Thread(new ThreadStart(this.Execute));
            _thread.Start();
        }


        ~FlyingCenterSystem()
        {
            End();
        }


        public static FlyingCenterSystem Instance
        {
            get
            {
                return _instance;
            }
        }
        public T GetFacade<T>()
        {
            T facade = default;
            Type facadeType = typeof(T);
            if (facadeType == typeof(AnonymousUserFacade))
            {
                if (_anonymousFacade is null)
                {
                    _anonymousFacade = new AnonymousUserFacade();
                }
                facade = (T)(object)_anonymousFacade;
            }
            else if (facadeType == typeof(LoggedInCustomerFacade))
            {
                if (_customerFacade is null)
                {
                    _customerFacade = new LoggedInCustomerFacade();
                }
                facade= (T)(object)_customerFacade;
            }
            else if (facadeType == typeof(LoggedInAirlineFacade))
            {
                if (_airlineFacade is null)
                {
                    _airlineFacade = new LoggedInAirlineFacade();
                }
                facade = (T)(object)_airlineFacade;
            }
            else if (facadeType == typeof(LoggedInAdministratorFacade))
            {
                if (_administratorFacade is null)
                {
                    _administratorFacade = new LoggedInAdministratorFacade();
                }
                facade = (T)(object)_administratorFacade;
            }


            return facade;
        }
        private void Execute()
        {
           
            //LoginToken<Administrator> tokenAdmin;
            LoginService loginService = new LoginService();
            bool result = loginService.TryAdminLogin("9999", "admin", out LoginToken<Administrator> tokenAdmin);
            if (result == true)
            {
                while (!_end)
                {
                    try
                    {
                        Thread.Sleep(_threadTime);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        break;
                    }

                    LoggedInAdministratorFacade af = GetFacade<LoggedInAdministratorFacade>();
                    lock (this)
                    {
                        af.TransferFlightsToHistory(tokenAdmin);
                    }
                }
            }
        }
        public void End()
        {
            _end = true;
            _thread.Interrupt();
            _thread.Join();
        }
    }
}
