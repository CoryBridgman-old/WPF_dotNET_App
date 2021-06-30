using System;
using System.Collections.Generic;
using System.Text;

/**
 * A class used for easily passing the program data between windows
 **/
namespace Midterm___Cory_Bridgman_991199354
{
    public class ProgramData
    {
        private List<Customer> customerlist;
        private int cList_id = 0;
        private List<Flights> flightsList;
        private int fList_id = 0;
        private Queue<Airlines> airlinesQueue;
        private int aQueue_id = 0;
        private Stack<Passenger> passengerStack;
        private int pStack_id = 0;
        private Dictionary<string, Logins> loginsDictionary;
        private int loginD_id = 0;
        private int isSuper = 0;
        private bool isQuitting = false;

        public ProgramData()
        {
            customerlist = new List<Customer>();
            flightsList = new List<Flights>();
            airlinesQueue = new Queue<Airlines>();
            passengerStack = new Stack<Passenger>();
            loginsDictionary = new Dictionary<string, Logins>();
            initializeAll();
            
        }
        public int newCustID()
        {
            return cList_id++;
        }
        public int newFlightID()
        {
            return fList_id++;
        }
        public int newAirlnID()
        {
            return aQueue_id++;
        }
        public int newPassID()
        {
            return pStack_id++;
        }
        public int newLoginID()
        {
            return loginD_id++;
        }
        public bool Quit
        {
            get { return isQuitting; }
            set { isQuitting = value; }
        }
        public int Super
        {
            get { return isSuper; }
            set { isSuper = value; }
        }
        public List<Customer> CustomerList
        {
            get { return customerlist; }
            set { customerlist = value; }
        }
        public List<Flights> FlightsList
        {
            get { return flightsList; }
            set { flightsList = value; }
        }
        public Queue<Airlines> AirlinesQueue
        {
            get { return airlinesQueue; }
            set { airlinesQueue = value; }
        }
        public Stack<Passenger> PassengerStack
        {
            get { return passengerStack; }
            set { passengerStack = value; }
        }
        public Dictionary<string, Logins> Users
        {
            get { return loginsDictionary; }
            set { loginsDictionary = value; }
        }

        public void initializeAll()
        {
            //User profiles
            loginsDictionary.Add("fathom", new Logins(loginD_id++, "fathom", "fathom", 0)); //0
            loginsDictionary.Add("super", new Logins(loginD_id++, "super", "super", 1)); //1
            loginsDictionary.Add("AnotherUser", new Logins(loginD_id++, "AnotherUser", "anotheruser", 0)); //2
            loginsDictionary.Add("tandom", new Logins(loginD_id++, "tandom", "axe", 0)); //3
            loginsDictionary.Add("xY2Kx", new Logins(loginD_id++, "xY2Kx", "2000", 0)); //4

            //Customers
            customerlist.Add(new Customer(cList_id++, "Bonnie", "123 America", "i fought the law", "911")); //0
            customerlist.Add(new Customer(cList_id++, "Clyde", "456 America", "and the law won", "911")); //1
            customerlist.Add(new Customer(cList_id++, "Grover", "Trash Can Unit#6", "get_at_me@g.rover", "2115334854")); //2
            customerlist.Add(new Customer(cList_id++, "Alphabet Boys", "[redacted]", "pcia_foia@ucia.gov", "+1-703–482–0623")); //3
            customerlist.Add(new Customer(cList_id++, "China Steve", "Spadina", "realCanada@ca.ca", "+10001231234")); //4

            //Airlines
            airlinesQueue.Enqueue(new Airlines(aQueue_id++, "Smackdown Air", "5-star forester", 666, "Veal")); //0
            airlinesQueue.Enqueue(new Airlines(aQueue_id++, "Sky Pajamas", "Boeing 112", 78, "Shorts (yours)")); //1
            airlinesQueue.Enqueue(new Airlines(aQueue_id++, "Comfort Cruise", "Harrier", 2, "Apple Seeds")); //2
            airlinesQueue.Enqueue(new Airlines(aQueue_id++, "Discomfort Cruise", "Harrierer", 3, "Apple Tree Seeds")); //3
            airlinesQueue.Enqueue(new Airlines(aQueue_id++, "Breeze Inc", "Nimbus", 121, "Lumberjack Special")); //4

            //Flights
            flightsList.Add(new Flights(fList_id++, 2, "Toronto", "Berlin", "Sep 02 2022", 14.5)); //0
            flightsList.Add(new Flights(fList_id++, 0, "Detroit", "Nashville", "Jan 03 2022", 2)); //1
            flightsList.Add(new Flights(fList_id++, 1, "Warsaw", "Bucharest", "Dec 07 2021", 3.5)); //2
            flightsList.Add(new Flights(fList_id++, 4, "Budapest", "Moscow", "Dec 31 2021", 24)); //3
            flightsList.Add(new Flights(fList_id++, 4, "Washington", "Jegrown", "Mar 15 2022", 72)); //4

            //Passengers
            passengerStack.Push(new Passenger(pStack_id++, 0, 0)); //0
            passengerStack.Push(new Passenger(pStack_id++, 1, 1)); //1
            passengerStack.Push(new Passenger(pStack_id++, 2, 2)); //2
            passengerStack.Push(new Passenger(pStack_id++, 3, 3)); //3
            passengerStack.Push(new Passenger(pStack_id++, 4, 2)); //4

        }

    }
}
