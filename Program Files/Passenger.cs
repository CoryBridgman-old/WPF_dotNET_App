using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm___Cory_Bridgman_991199354
{

    public class Passenger
    {
        private int _id;
        private int _customerID; //matches customer id
        private int _flightID; // matches flight id

        public Passenger(int id, int customerID, int flightID)
        {
            _id = id;
            _customerID = customerID;
            _flightID = flightID;
        }

        public int ID
        {
            get { return _id; }
        }
        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }
        public int FlightID
        {
            get { return _flightID; }
            set { _flightID = value; }
        }

        public string formatPassengerID()
        {
            return "Passenger " + _id.ToString();
        }
    }
}
