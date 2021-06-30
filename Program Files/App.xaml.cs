using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Midterm___Cory_Bridgman_991199354
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string test = "test string on app level";
        private static string testPriv = "test string private static";

        public string TEST
        {
            get { return test; }
        }
        public string TESTPRIV
        {
            get { return testPriv; }
        }
    }
}
