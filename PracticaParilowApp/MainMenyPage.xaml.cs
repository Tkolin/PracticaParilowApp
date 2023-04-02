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

namespace PracticaParilowApp
{
    /// <summary>
    /// Логика взаимодействия для MainMenyPage.xaml
    /// </summary>
    public partial class MainMenyPage : Page
    {
        public Users users;
        public MainMenyPage(Users user)
        {
            InitializeComponent();
            this.users = user;
        }

        private void btnApplications_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ApplicationPage(users));
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ApplicationPage(users));

        }

        private void btnComplaint_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplaintPage(users));

        }

        private void btnAddComplaint_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplaintAddEditPage(users));

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
