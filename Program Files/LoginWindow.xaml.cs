using System;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ProgramData data = new ProgramData();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (data.Users.ContainsKey(userName.Text))
            {
                if(userPw.Password == data.Users[userName.Text].Password)
                {
                    data.Super = data.Users[userName.Text].Super;
                    HomePage testWin = new HomePage(ref data);
                    loginWindow.Hide();
                    testWin.ShowDialog();
                    if(!data.Quit) loginWindow.Show();
                }
                else
                {
                    MessageBox.Show("Password is incorrect", "Invalid Password",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("User doesn't exist or username is incorrect", "Username doesn't exist",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (data.Quit) return;

            var closing = MessageBox.Show("Do you want to exit?", "Quitting Application",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (closing == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
