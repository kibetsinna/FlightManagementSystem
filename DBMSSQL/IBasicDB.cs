using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.DBConnection
{
    public interface IBasicDB<T> where T: PocoLogin.IPoco
    {
        T Get(int id);
        List<T> GetAll();
        int Add(T t);
        void Remove(T t);
        void Update(T t);

    }
}
