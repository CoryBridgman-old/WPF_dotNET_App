
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
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Window
    {

        private ProgramData data;

        public CustomersPage(ref ProgramData d)
        {
            data = d;
            InitializeComponent();

            var people = from p in data.CustomerList
                         select p.Name;

            listNames.DataContext = people;
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
        private void listNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dex = listNames.SelectedIndex;
            if (!(dex >= 0)) return;

            var target = from p in data.CustomerList
                         select p;
            int c_id = target.ElementAt(dex).ID;

            var selectedHuman = from hu in data.CustomerList
                                where hu.ID == c_id
                                select hu;

            foreach (var hue in selectedHuman)
            {
                nameBox.Text = hue.Name;
                addressBox.Text = hue.Address;
                emailBox.Text = hue.Email;
                phoneBox.Text = hue.Phone;
            }
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            if (nameBox.Text.Trim() == "" || addressBox.Text.Trim() == "" || emailBox.Text.Trim() == "" || phoneBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                data.CustomerList.Add(new Customer(data.newCustID(), nameBox.Text, addressBox.Text, emailBox.Text, phoneBox.Text));
                var people = from p in data.CustomerList
                             select p.Name;
                listNames.DataContext = people;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            if (nameBox.Text.Trim() == "" || addressBox.Text.Trim() == "" || emailBox.Text.Trim() == "" || phoneBox.Text.Trim() == "")
            {
                MessageBox.Show("There are missing fields. Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int dex = listNames.SelectedIndex;
                if(!(dex >= 0))
                {
                    MessageBox.Show("Please select a name from the list.", "Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    var target = from p in data.CustomerList
                                 select p;
                    int c_id = target.ElementAt(dex).ID;
                    data.CustomerList[dex] = new Customer(c_id, nameBox.Text.Trim(), addressBox.Text.Trim(), emailBox.Text.Trim(), phoneBox.Text.Trim());
                    var people = from p in data.CustomerList
                                 select p.Name;
                    listNames.DataContext = people;
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (isIllegalAccess()) return;
            int dex = listNames.SelectedIndex;
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
                int c_id = data.CustomerList[dex].ID;

                //Delete passenger that contains customer
                Stack<Passenger> waitingRoom = new Stack<Passenger>();
                int numPass = data.PassengerStack.Count();
                for (int i = 0; i < numPass; i++)
                {
                    if (data.PassengerStack.Peek().CustomerID == c_id) 
                    {
                        data.PassengerStack.Pop();
                    }
                    else waitingRoom.Push(data.PassengerStack.Pop());
                }
                for (int i = 0; i < numPass - 1; i++)
                {
                    data.PassengerStack.Push(waitingRoom.Pop());
                }

                data.CustomerList.RemoveAt(dex);
                var people = from p in data.CustomerList
                             select p.Name;
                listNames.DataContext = people;
            }
        }

        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            Insert_Click(sender, e);
        }
        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update_Click(sender, e);
        }
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click(sender, e);
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

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutpage = new About(ref data);
            aboutpage.ShowDialog();
        }
    }
}
