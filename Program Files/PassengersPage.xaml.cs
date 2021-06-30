using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Midterm___Cory_Bridgman_991199354
{
    /// <summary>
    /// Interaction logic for PassengersPage.xaml
    /// </summary>
    public partial class PassengersPage : Window
    {
        private ProgramData data;
        public PassengersPage(ref ProgramData d)
        {
            data = d;
            InitializeComponent();

            var passengers = from pass in data.PassengerStack
                              select pass.formatPassengerID();
            passengersList.DataContext = passengers;

            var customers = from c in data.CustomerList
                            select c.Name;
            customerListbox.DataContext = customers;

            var flights = from f in data.FlightsList
                          select f.formatFlightID();
            flightsListbox.DataContext = flights;
        }

        private void mQuit_Click(object sender, RoutedEventArgs e)
        {
            var deletePop = MessageBox.Show("Are you sure you wish to Quit?", "Quitting",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (deletePop == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                data.Quit = true;
                Application.Current.Shutdown();
            }
        }
        private void aAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutpage = new About(ref data);
            aboutpage.ShowDialog();
        }
        public bool isIllegalAccess()
        {
            if (data.Super != 1)
            {
                MessageBox.Show("This action requires SUPER USER privileges", "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int cust_dex = customerListbox.SelectedIndex;
            int flt_dex = flightsListbox.SelectedIndex;
            if (!(cust_dex >= 0) || !(flt_dex >= 0))
            {
                MessageBox.Show("Please ensure you have made a selection from the Customer and Flights boxes.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int c_id = data.CustomerList[cust_dex].ID;
            int f_id = data.FlightsList[flt_dex].ID;
            data.PassengerStack.Push(new Passenger(data.newPassID(), c_id, f_id));


            //update display
            var passengers = from pass in data.PassengerStack
                              select pass.formatPassengerID();
            passengersList.DataContext = passengers;
        }
        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert_Click(sender, e);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int pass_dex = passengersList.SelectedIndex;
            int cust_dex = customerListbox.SelectedIndex;
            int flt_dex = flightsListbox.SelectedIndex;
            if (!(pass_dex >= 0) || !(cust_dex >= 0) || !(flt_dex >= 0))
            {
                MessageBox.Show("Please ensure you have made a selection from each box.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int c_id = data.CustomerList[cust_dex].ID;
            int f_id = data.FlightsList[flt_dex].ID;

            Stack<Passenger> waitingRoom = new Stack<Passenger>();
            int numPass = data.PassengerStack.Count();
            for (int i = 0; i < numPass; i++)
            {
                if (i == pass_dex)
                {
                    data.PassengerStack.Peek().CustomerID = c_id;
                    data.PassengerStack.Peek().FlightID = f_id;
                }
                waitingRoom.Push(data.PassengerStack.Pop());
            }
            for (int i = 0; i < numPass; i++)
            {
                data.PassengerStack.Push(waitingRoom.Pop());
            }
        }
        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update_Click(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int pass_dex = passengersList.SelectedIndex;
            if (!(pass_dex >= 0))return;
            var deletePop = MessageBox.Show("Are you sure you wish to delete?", "Deleting",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (deletePop == MessageBoxResult.No)
            {
                return;
            }

            Stack<Passenger> waitingRoom = new Stack<Passenger>();
            int numPass = data.PassengerStack.Count();
            for (int i = 0; i < numPass; i++)
            {
                if (i == pass_dex) data.PassengerStack.Pop();
                else waitingRoom.Push(data.PassengerStack.Pop());
            }
            for (int i = 0; i < numPass-1; i++)
            {
                data.PassengerStack.Push(waitingRoom.Pop());
            }

            //update display
            var passengers = from pass in data.PassengerStack
                             select pass.formatPassengerID();
            passengersList.DataContext = passengers;

        }
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }


        private void passengersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pass_dex = passengersList.SelectedIndex;
            if (!(pass_dex >= 0)) return;

            Stack<Passenger> waitingRoom = new Stack<Passenger>();
            int p_id = -1;
            int numPass = data.PassengerStack.Count();
            for(int i = 0; i < numPass; i++)
            {
                if(i == pass_dex) p_id = data.PassengerStack.Peek().ID;
                waitingRoom.Push(data.PassengerStack.Pop());
            }
            for (int i = 0; i < numPass; i++)
            {
                data.PassengerStack.Push(waitingRoom.Pop());
            }

            if (p_id == -1) return;
            var target_passenger = (from pa in data.PassengerStack
                                    where pa.ID == p_id
                                    select pa).First();

            int c_id = target_passenger.CustomerID;
            int f_id = target_passenger.FlightID;

            for (int i = 0; i < data.CustomerList.Count; i++)  if (data.CustomerList[i].ID == c_id) customerListbox.SelectedIndex = i;
            for (int i = 0; i < data.FlightsList.Count; i++)  if (data.FlightsList[i].ID == f_id) flightsListbox.SelectedIndex = i;
            
        }
    }
}
