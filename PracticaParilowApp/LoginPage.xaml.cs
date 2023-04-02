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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<Users> users;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnAddComplaint_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplaintAddEditPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (loginSearch() != null)
            {
                NavigationService.Navigate(new MainMenyPage(loginSearch()));
            }
            else
                MessageBox.Show("Ошибка входа");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            users = ppDataBaseEntities.GetContext().Users.ToList();
        }
        private Users loginSearch()
        {
            foreach (Users checkUser in users)
            {
                if (checkUser.Login.ToLower() == TBoxLogin.Text.ToLower() &&
                    checkUser.Password == TBoxPassword.Text)
                {
                    return checkUser;
                }

            }
            return null;
        }
    }
}
