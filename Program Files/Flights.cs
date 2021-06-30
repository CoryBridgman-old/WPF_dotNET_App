using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm___Cory_Bridgman_991199354
{
    public class Flights
    {
        private int _id;
        private int _airlineID; //should match id of airline it belongs to
        private string _departureCity;
        private string _destinationCity;
        private string _departureDate;
        private double _flightTime;
    
        public Flights(int id, int a_id, string depart, string arrive, string date, double flightTime)
        {
            _id = id;
            _airlineID = a_id;
            _departureCity = depart;
            _destinationCity = arrive;
            _departureDate = date;
            _flightTime = flightTime;
        }
        public int ID
        {
            get { return _id; }
        }
        public int AirlineID
        {
            get { return _airlineID; }
            set { _airlineID = value; }
        }
        public string DepartureCity
        {
            get { return _departureCity; }
            set { _departureCity = value; }
        }
        public string DestinationCity
        {
            get { return _destinationCity; }
            set { _destinationCity = value; }
        }
        public string DepartureDate
        {
            get { return _departureDate; }
            set { _departureDate = value; }
        }
        public double FlightTime
        {
            get { return _flightTime; }
            set { _flightTime = value; }
        }
        public string formatFlightID()
        {
            return "Flight " + _id.ToString();
        }

    }
}
