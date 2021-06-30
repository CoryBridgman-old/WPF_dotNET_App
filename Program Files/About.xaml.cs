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
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private ProgramData data;
        public About(ref ProgramData d)
        {
            data = d;
            InitializeComponent();
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
    }
}
