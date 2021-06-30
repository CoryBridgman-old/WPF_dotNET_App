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
    /// Interaction logic for FlightsPage.xaml
    /// </summary>
    public partial class FlightsPage : Window
    {
        private ProgramData data;

        public FlightsPage(ref ProgramData d)
        {
            data = d;
            InitializeComponent();

            var flights = from f in data.FlightsList
                          select f.formatFlightID();
            listFlights.DataContext = flights;

            var airlines = from a in data.AirlinesQueue
                           select a.Name;
            listAirlines.DataContext = airlines;
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

        private void mAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutpage = new About(ref data);
            aboutpage.ShowDialog();
        }

        private void listFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dex = listFlights.SelectedIndex;
            if (!(dex >= 0)) return;

            int f_id = data.FlightsList[dex].ID;

            var selectedFlight = from fl in data.FlightsList
                                  where fl.ID == f_id
                                  select fl;

            for(int i = 0; i < data.AirlinesQueue.Count; i++)
            {
                if (data.AirlinesQueue.Peek().ID == selectedFlight.First().AirlineID)
                {
                    listAirlines.SelectedIndex = i;
                }
                data.AirlinesQueue.Enqueue(data.AirlinesQueue.Dequeue());
            }

            foreach (var f in selectedFlight)
            {
                departureBox.Text = f.DepartureCity;
                destinationBox.Text = f.DestinationCity;
                dateBox.Text = f.DepartureDate;
                durationBox.Text = f.FlightTime.ToString();
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            if (departureBox.Text.Trim() == "" || destinationBox.Text.Trim() == "" || dateBox.Text.Trim() == "" || durationBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int dex = listAirlines.SelectedIndex;
                if (!(dex >= 0))
                {
                    MessageBox.Show("Please select an airline.", "Details missing.", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                int airline_id = -1;
                for (int i = 0; i < data.AirlinesQueue.Count; i++)
                {
                    if (i == dex)
                    {
                        airline_id = data.AirlinesQueue.Peek().ID;
                    }
                    data.AirlinesQueue.Enqueue(data.AirlinesQueue.Dequeue());
                }
                if(airline_id == -1)
                {
                    MessageBox.Show("Airline not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double flightTime;
                if (!(double.TryParse(durationBox.Text, out flightTime)))
                {
                    MessageBox.Show("Please enter a valid flight duration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                data.FlightsList.Add(new Flights(data.newFlightID(), airline_id, departureBox.Text, destinationBox.Text, dateBox.Text, flightTime));

                var flights = from f in data.FlightsList
                              select f.formatFlightID();
                listFlights.DataContext = flights;

                var airlines = from a in data.AirlinesQueue
                               select a.Name;
                listAirlines.DataContext = airlines;
            }
        }
        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert_Click(sender, e);
        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            if (departureBox.Text.Trim() == "" || destinationBox.Text.Trim() == "" || dateBox.Text.Trim() == "" || durationBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Selected flight id
                int fl_dex = listFlights.SelectedIndex;
                if (!(fl_dex >= 0))
                {
                    MessageBox.Show("Please select a flight.", "Details missing.", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int flight_id = data.FlightsList[fl_dex].ID;

                //Selected airline id
                int air_dex = listAirlines.SelectedIndex;
                if (!(air_dex >= 0))
                {
                    MessageBox.Show("Please select an airline.", "Details missing.", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                int airline_id = -1;
                for (int i = 0; i < data.AirlinesQueue.Count; i++)
                {
                    if (i == air_dex)
                    {
                        airline_id = data.AirlinesQueue.Peek().ID;
                    }
                    data.AirlinesQueue.Enqueue(data.AirlinesQueue.Dequeue());
                }
                if (airline_id == -1)
                {
                    MessageBox.Show("Airline not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                double flightTime;
                if (!(double.TryParse(durationBox.Text, out flightTime)))
                {
                    MessageBox.Show("Please enter a valid flight duration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                data.FlightsList[fl_dex] = (new Flights(flight_id, airline_id, departureBox.Text, destinationBox.Text, dateBox.Text, flightTime));

                var flights = from f in data.FlightsList
                              select f.formatFlightID();
                listFlights.DataContext = flights;

                var airlines = from a in data.AirlinesQueue
                               select a.Name;
                listAirlines.DataContext = airlines;
            }
        }
        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update_Click(sender, e);
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int fl_dex = listFlights.SelectedIndex;
            if (!(fl_dex >= 0))
            {
                MessageBox.Show("Please select a flight.", "Details missing.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deletePop = MessageBox.Show("Are you sure you wish to delete?", "Deleting",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (deletePop == MessageBoxResult.No)
            {
                return;
            }

            int f_id = data.FlightsList[fl_dex].ID;
            //Delete passenger that contains flight
            Stack<Passenger> waitingRoom = new Stack<Passenger>();
            int numPass = data.PassengerStack.Count();
            for (int i = 0; i < numPass; i++)
            {
                if (data.PassengerStack.Peek().CustomerID == f_id)
                {
                    data.PassengerStack.Pop();
                }
                else waitingRoom.Push(data.PassengerStack.Pop());
            }
            for (int i = 0; i < numPass - 1; i++)
            {
                data.PassengerStack.Push(waitingRoom.Pop());
            }

            data.FlightsList.RemoveAt(fl_dex);
            var flights = from f in data.FlightsList
                          select f.formatFlightID();
            listFlights.DataContext = flights;

            var airlines = from a in data.AirlinesQueue
                           select a.Name;
            listAirlines.DataContext = airlines;
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }
    }
}
