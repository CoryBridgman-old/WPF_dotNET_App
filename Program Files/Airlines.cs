using System;
using System.Collections.Generic;
using System.Text;

namespace Midterm___Cory_Bridgman_991199354
{
    public class Airlines
    {
        private int _id;
        private string _name;
        private string _airplane;
        private int _seatsAvailable; //entered as per specifications, would normally make attribute of flight
        private string _mealAvailable;

        public Airlines(int id, string name, string airplane, int seatsAvailable, string mealAvailable)
        {
            _id = id;
            _name = name;
            _airplane = airplane;
            _seatsAvailable = seatsAvailable;
            _mealAvailable = mealAvailable;
        }

        public int ID
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Airplane
        {
            get { return _airplane; }
            set { _airplane = value; }
        }
        public int SeatsAvailable
        {
            get { return _seatsAvailable; }
            set { _seatsAvailable = value; }
        }
        public string MealsAvailable
        {
            get { return _mealAvailable; }
            set { _mealAvailable = value; }
        }
    }

}
