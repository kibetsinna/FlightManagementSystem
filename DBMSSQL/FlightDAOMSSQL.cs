using FlightManagementSystem.PocoLogin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.DBConnection
{
    public class FlightDAOMSSQL : IFlightDAO
    {
        static SqlConnection conSQL = new SqlConnection(SQLConnection.conStr);
        public int Add(Flight t)
        {
            int result = 0;
            int airlineCompanyId = t.AirlineCompanyId;
            int originCountryCode = t.OriginCountryCode;
            int destinationCountryCode = t.DestinationCountryCode;
            DateTime departureTime = t.DepartureTime;
            DateTime landingTime = t.LandingTime;
            int remainingTickets = t.RemainingTickets;
            int flightStatus = t.FlightStatus;
            Flight f = GetByAllFields(airlineCompanyId, originCountryCode, destinationCountryCode, departureTime, landingTime);

            if (f is null)
            {
                SQLConnection.SQLOpen(conSQL);
                string cmdStr = $"INSERT INTO Flights VALUES({airlineCompanyId},"+
                    $"{originCountryCode},{destinationCountryCode},'{departureTime}'," +
                    $"'{landingTime}',{remainingTickets},{flightStatus});SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                SQLConnection.SQLClose(conSQL);
            }

            else
            {
                throw new FlighAlreadyExistsException($"The flight already exists with ID {f.Id}");
            }

            return result;
        }

        public Flight Get(int id)
        {
            SQLConnection.SQLOpen(conSQL);
            Flight f = null;
            string cmdStr = $"SELECT * FROM Flights WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return f;
        }
        public Flight GetByAllFields(int airlineCompanyId, int originCountryCode, int destinationCountryCode, DateTime departureTime, DateTime landingTime)
        {
            SQLConnection.SQLOpen(conSQL);
            Flight f = null;
            string cmdStr = $"SELECT * FROM Flights WHERE AIRLINECOMPANY_ID = {airlineCompanyId} AND " +
                $"ORIGIN_COUNTRY_CODE = {originCountryCode} AND DESTINATION_COUNTRY_CODE = {destinationCountryCode} AND " +
                $"DEPARTURE_TIME = '{departureTime}' AND LANDING_TIME = '{landingTime}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return f;
        }
        public List<Flight> GetAll()
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> FlightsVacancy = new Dictionary<Flight, int>();
            List<Flight> allFlights = GetAll();
            foreach(Flight f in allFlights)
            {
                if(f.RemainingTickets>0&&f.FlightStatus==1)
                    FlightsVacancy.Add(f, f.Id);
            }
            return FlightsVacancy;
        }

        

            

        public List<Flight> GetFlightsByCustomer(Customer customer)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT Flights.ID,Flights.AIRLINECOMPANY_ID, Flights.ORIGIN_COUNTRY_CODE," +
                $"Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME,Flights.LANDING_TIME,Flights.REMAINING_TICKETS," +
                $"Flights.FLIGHT_STATUS_ID FROM  Tickets JOIN Flights ON Tickets.FLIGHT_ID = Flights.ID WHERE Tickets.CUSTOMER_ID = {customer.Id}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }
        public List<Flight> GetFlightsByAirlineCompany(AirlineCompany airlineCompany)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights WHERE AIRLINECOMPANY_ID = {airlineCompany.Id}";

            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public List<Flight> GetFlightsByDepartureTime(DateTime departureDate)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights WHERE DEPARTURE_TIME = '{departureDate}'";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public List<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights WHERE DESTINATION_COUNTRY_CODE = {countryCode}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public List<Flight> GetFlightsByLandingTime(DateTime landingDate)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights WHERE LANDING_TIME = '{landingDate}'";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public List<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights WHERE ORIGIN_COUNTRY_CODE = {countryCode}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }

        public void Remove(Flight t)
        {
            Flight f = Get(t.Id);
            if (f is null)
            {
                throw new FlightNotFoundException($"The flight  with id {t.Id} does not exist");
            }
            SQLConnection.SQLOpen(conSQL);
            string cmdStr = $"DELETE FROM Flights WHERE ID = { t.Id }";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnection.SQLClose(conSQL);
        }

        public void Update(Flight t)
        {
            Flight f = Get(t.Id);
            if (f is null)
            {
                throw new FlightNotFoundException($"The flight  with id {t.Id} does not exist");
            }
            SQLConnection.SQLOpen(conSQL);

            string cmdStr = $"UPDATE Flights SET AIRLINECOMPANY_ID = {t.AirlineCompanyId}," +
              $"ORIGIN_COUNTRY_CODE = {t.OriginCountryCode}, DESTINATION_COUNTRY_CODE = {t.DestinationCountryCode}," +
              $"DEPARTURE_TIME = '{t.DepartureTime}', LANDING_TIME = '{t.LandingTime}',"+
              $"REMAINING_TICKETS ={t.RemainingTickets}, FLIGHT_STATUS = {t.FlightStatus} WHERE ID = {t.Id}";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnection.SQLClose(conSQL);
        }
        public void DeleteAll()
        {
            SQLConnection.SQLOpen(conSQL);
            string cmdStr = $"DELETE FROM Flights ";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnection.SQLClose(conSQL);
        }
        public void DeleteAllHistory()
        {
            SQLConnection.SQLOpen(conSQL);
            string cmdStr = $"DELETE FROM FlightsHistory ";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnection.SQLClose(conSQL);
        }
        public List<Flight> GetFlightsToTransfer()
        {
            SQLConnection.SQLOpen(conSQL);
            List<Flight> flights = new List<Flight>();
            string cmdStr = $"SELECT * FROM Flights where FLIGHT_STATUS = 3 AND LANDING_TIME <=(SELECT DATEADD(HOUR,{SQLConnection.transferTime}, GETDATE()))";
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Flight f = null;
                    while (reader.Read())
                    {
                        f = new Flight
                        {
                            Id = (int)reader["ID"],
                            AirlineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            OriginCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            DestinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                            LandingTime = (DateTime)reader["LANDING_TIME"],
                            RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            FlightStatus = (int)reader["FLIGHT_STATUS"]
                        };

                        flights.Add(f);
                    }
                }
            }
            SQLConnection.SQLClose(conSQL);
            return flights;
        }
        public void TransferFlightToHistory(Flight flight)
        {
            Flight f = Get(flight.Id);
            if (f is null)
            {
                throw new FlightNotFoundException($"The flight  with id {flight.Id} does not exist");
            }
            int airlineCompanyId = flight.AirlineCompanyId;
            int originCountryCode = flight.OriginCountryCode;
            int destinationCountryCode = flight.DestinationCountryCode;
            DateTime departureTime = flight.DepartureTime;
            DateTime landingTime = flight.LandingTime;
      
            SQLConnection.SQLOpen(conSQL);
            string cmdStr = $"INSERT INTO FlightsHistory VALUES({flight.Id},{ airlineCompanyId}," +
                    $"{originCountryCode},{destinationCountryCode},'{departureTime}','{landingTime}')";
           
            using (SqlCommand cmd = new SqlCommand(cmdStr, conSQL))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnection.SQLClose(conSQL);
          
        }
    }
}
