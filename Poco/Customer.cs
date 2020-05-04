using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.PocoLogin
{
    public class Customer : IPoco, IUser
 
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _address;
        private string _phoneNo;
        private string _creditCardNumber;
        public int Id
        {

            get { return _id; }
            set { _id = value; }

        }
        public string FirstName
        {

            get { return _firstName; }
            set { _firstName = value; }

        }
        public string LastName
        {

            get { return _lastName; }
            set { _lastName = value; }

        }
        public string UserName
        {

            get { return _userName; }
            set { _userName = value; }


        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }
        public string CreditCardNumber
        {
            get { return _creditCardNumber; }
            set { _creditCardNumber = value; }
        }
        public Customer() { }

        public Customer(string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditCardNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;
            _password = password;
            _address = address;
            _phoneNo = phoneNo;
            _creditCardNumber = creditCardNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is Customer c &&
                   Id == c.Id;
        }

    

        public override string ToString()
        {
            return ($"Customer {Id} ,{FirstName}, {LastName}, {UserName}, {Password}, {Address}, {PhoneNo}, {CreditCardNumber}");
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(Customer c1, Customer c2)
        {
            if (c1 == null && c2 == null)
                return true;
            if (c1 == null && c2 != null || c2 == null && c1 != null)
                return false;
            return (c1.Id == c2.Id);
        }
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }
    }
}
