using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.PocoLogin
{
    public class FlightStatus : IPoco, IUser
 
    {
        private int _id;
        private string _statusName;

        public int Id
        {

            get { return _id; }
            set { _id = value; }

        }
        public string StatusName
        {

            get { return _statusName; }
            set { _statusName = value; }

        }

        public FlightStatus() { }
        public FlightStatus(int id,string statusName)
        {
            _id = id;
            _statusName = statusName;

        }

        public override bool Equals(object obj)
        {
            return obj is FlightStatus fs &&
                   Id == fs.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return ($"FlightStatus {Id} ,{StatusName}");
        }



        public static bool operator ==(FlightStatus fs1, FlightStatus fs2)
        {
            return (fs1.Id == fs2.Id);
        }
        public static bool operator !=(FlightStatus fs1, FlightStatus fs2)
        {
            return !(fs1 == fs2);
        }
    }
}
