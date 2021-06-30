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
    /// Interaction logic for AirlinesPage.xaml
    /// </summary>
    public partial class AirlinesPage : Window
    {
        private ProgramData data;
        public AirlinesPage(ref ProgramData d)
        {
            data = d;
            InitializeComponent();

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

        /**
         * Checkboxes, glorious checkboxes
         **/
        private void foodVeal_Checked(object sender, RoutedEventArgs e)
        {
            foodBox.Text = foodVeal.Content.ToString();
        }

        private void foodShorts_Checked(object sender, RoutedEventArgs e)
        {
            foodBox.Text = foodShorts.Content.ToString();
        }

        private void foodSeeds_Checked(object sender, RoutedEventArgs e)
        {
            foodBox.Text = foodSeeds.Content.ToString();
        }

        private void foodTree_Checked(object sender, RoutedEventArgs e)
        {
            foodBox.Text = foodTree.Content.ToString();
        }

        private void foodLumber_Checked(object sender, RoutedEventArgs e)
        {
            foodBox.Text = foodLumber.Content.ToString();
        }

        private void planeSmack_Checked(object sender, RoutedEventArgs e)
        {
            planeBox.Text = planeSmack.Content.ToString();
        }

        private void plane112_Checked(object sender, RoutedEventArgs e)
        {
            planeBox.Text = plane112.Content.ToString();
        }

        private void planeHar_Checked(object sender, RoutedEventArgs e)
        {
            planeBox.Text = planeHar.Content.ToString();
        }

        private void planeHarar_Checked(object sender, RoutedEventArgs e)
        {
            planeBox.Text = planeHarar.Content.ToString();
        }

        private void planeNimbus_Checked(object sender, RoutedEventArgs e)
        {
            planeBox.Text = planeNimbus.Content.ToString();
        }

        private void listAirlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dex = listAirlines.SelectedIndex;
            if (!(dex >= 0)) return;
            var target = from a in data.AirlinesQueue
                         select a;
            int a_id = target.ElementAt(dex).ID;
            var selectedAirline = from ar in data.AirlinesQueue
                                  where ar.ID == a_id
                                  select ar;

            foreach(var a in selectedAirline)
            {
                airnameBox.Text = a.Name;
                planeBox.Text = a.Airplane;
                seatsBox.Text = a.SeatsAvailable.ToString();
                foodBox.Text = a.MealsAvailable;
            }
            foreach(RadioButton rbn in foodRadioButtons.Children)
            {
                if (rbn.Content.ToString() == selectedAirline.First().MealsAvailable) rbn.IsChecked = true;
            }
            foreach(RadioButton rbn2 in planeRadioButtons.Children)
            {
                if (rbn2.Content.ToString() == selectedAirline.First().Airplane) rbn2.IsChecked = true;
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            if (airnameBox.Text.Trim() == "" || planeBox.Text.Trim() == "" || seatsBox.Text.Trim() == "" || foodBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int numSeats;
                if(!(int.TryParse(seatsBox.Text, out numSeats)))
                {
                    MessageBox.Show("Please enter a valid number of seats.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                data.AirlinesQueue.Enqueue(new Airlines(data.newAirlnID(), airnameBox.Text, planeBox.Text, numSeats, foodBox.Text));
                var airlines = from a in data.AirlinesQueue
                               select a.Name;
                listAirlines.DataContext = airlines;
            }
        }
        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert_Click(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int dex = listAirlines.SelectedIndex;
            if (!(dex >= 0))
            {
                MessageBox.Show("Please select a name from the list.", "Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                var deletePop = MessageBox.Show("Are you sure you wish to delete?", "Deleting",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (deletePop == MessageBoxResult.No)
                {
                    return;
                }
                int kill_id = data.AirlinesQueue.ElementAt(dex).ID;

                //Get flight ids
                var flight = (from f in data.FlightsList
                             where f.AirlineID == kill_id
                             select f).ToList();
                foreach(var fl in flight)
                {
                    int f_id = fl.ID;
                    //Delete passenger that contains flight
                    Stack<Passenger> waitingRoom = new Stack<Passenger>();
                    int numPass = data.PassengerStack.Count();
                    for (int i = 0; i < numPass; i++)
                    {
                        if (data.PassengerStack.Peek().FlightID == f_id)
                        {
                            data.PassengerStack.Pop();
                        }
                        else waitingRoom.Push(data.PassengerStack.Pop());
                    }
                    numPass = waitingRoom.Count();
                    for (int i = 0; i < numPass; i++)
                    {
                        data.PassengerStack.Push(waitingRoom.Pop());
                    }
                }

                foreach(var flr in flight)
                {
                    //remove flight
                    data.FlightsList.Remove(flr);
                }


                //Remove airline
                int l = data.AirlinesQueue.Count();
                for (int i = 0; i < l; i++)
                {
                    if (data.AirlinesQueue.Peek().ID == kill_id)  data.AirlinesQueue.Dequeue();
                    else data.AirlinesQueue.Enqueue(data.AirlinesQueue.Dequeue());
                }

                var airlines = from a in data.AirlinesQueue
                               select a.Name;
                listAirlines.DataContext = airlines;
            }
        }
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int dex = listAirlines.SelectedIndex;
            if (!(dex >= 0))
            {
                MessageBox.Show("Please select a name from the list.", "Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (airnameBox.Text.Trim() == "" || planeBox.Text.Trim() == "" || seatsBox.Text.Trim() == "" || foodBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int numSeats;
                if (!(int.TryParse(seatsBox.Text, out numSeats)))
                {
                    MessageBox.Show("Please enter a valid number of seats.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int update_id = data.AirlinesQueue.ElementAt(dex).ID;
                int l = data.AirlinesQueue.Count();
                for (int i = 0; i < l; i++)
                {
                    if (data.AirlinesQueue.Peek().ID == update_id)
                    {
                        Airlines a = new Airlines(data.AirlinesQueue.Dequeue().ID, airnameBox.Text, planeBox.Text, numSeats, foodBox.Text);
                        data.AirlinesQueue.Enqueue(a);
                    }
                    else data.AirlinesQueue.Enqueue(data.AirlinesQueue.Dequeue());
                }

                var airlines = from a in data.AirlinesQueue
                               select a.Name;
                listAirlines.DataContext = airlines;
            }
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update_Click(sender, e);
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
    }
}
