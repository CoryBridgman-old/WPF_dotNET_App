using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Midterm___Cory_Bridgman_991199354
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private ProgramData data;

        public HomePage(ref ProgramData d)
        {
            data = d;
            InitializeComponent();
            
        }

        private void homeWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data.Quit) return;
            var closing = MessageBox.Show("Do you want to leave? You will need to login again.", "Leaving Application",
                    MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (closing == MessageBoxResult.No)
            {
                e.Cancel = true;
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


        private void customersBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomersPage customerPage = new CustomersPage(ref data);
            customerPage.ShowDialog();
        }
        private void CustBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomersPage customerPage = new CustomersPage(ref data);
            customerPage.ShowDialog();
        }


        private void FlBtn_Click(object sender, RoutedEventArgs e)
        {
            FlightsPage flightsPage = new FlightsPage(ref data);
            flightsPage.ShowDialog();
        }
        private void flightsBtn_Click(object sender, RoutedEventArgs e)
        {
            FlightsPage flightsPage = new FlightsPage(ref data);
            flightsPage.ShowDialog();
        }


        private void airlinesBtn_Click(object sender, RoutedEventArgs e)
        {
            AirlinesPage airlinesPage = new AirlinesPage(ref data);
            airlinesPage.ShowDialog();
        }

        private void AirlnBtn_Click(object sender, RoutedEventArgs e)
        {
            AirlinesPage airlinesPage = new AirlinesPage(ref data);
            airlinesPage.ShowDialog();
        }


        private void PassBtn_Click(object sender, RoutedEventArgs e)
        {
            PassengersPage passengersPage = new PassengersPage(ref data);
            passengersPage.ShowDialog();
        }
        private void passengersBtn_Click(object sender, RoutedEventArgs e)
        {
            PassengersPage passengersPage = new PassengersPage(ref data);
            passengersPage.ShowDialog();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutpage = new About(ref data);
            aboutpage.ShowDialog();
        }
    }
}
