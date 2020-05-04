using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.PocoLogin
{
    public class Administrator: IUser
    {
        private string _userName;
        private string _password;
        public string UserName { get { return _userName; } }
        public string PassWord { get { return _password; } }
        public Administrator()
        {
            _userName = "Admin";
            _password = "9999";

        }
      
    }
}
