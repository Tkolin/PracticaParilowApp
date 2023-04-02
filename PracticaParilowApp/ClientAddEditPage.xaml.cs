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
    /// Логика взаимодействия для ClientAddEditPage.xaml
    /// </summary>
    public partial class ClientAddEditPage : Page
    {
        Clients client;
        bool edit;
        public ClientAddEditPage()
        {
            InitializeComponent();
            this.client = new Clients();
            this.edit = false;
        }

        public ClientAddEditPage(Clients client)
        {
            InitializeComponent();
            this.client = client;
            this.edit = true;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            client.FirstName = tBoxFirstName.Text;
            client.LastName = tBoxLastName.Text;
            client.Patronymic = tBoxPatronymic.Text;

            client.Users = cBoxLogin.SelectedItem as Users;

            client.Email = tBoxEmail.Text;
            client.PhoneNumbers = tBoxPhoneNumber.Text;

            client.Adresses = cBoxAdress.SelectedItem as Adresses;
            client.AdressNumber = Convert.ToInt32(tBoxNumberAdress.Text);
            client.ApartmentNumber = Convert.ToInt32(tBoxNumberAportament.Text);
        
            if(!edit)
            {
                try
                {
                    ppDataBaseEntities.GetContext().Clients.Add(client);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {
                ppDataBaseEntities.GetContext().SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBoxAdress.SelectedValuePath = "ID";
            cBoxAdress.DisplayMemberPath = "Name";
            cBoxAdress.ItemsSource = ppDataBaseEntities.GetContext().Adresses.ToList();

            cBoxLogin.SelectedValuePath = "ID";
            cBoxLogin.DisplayMemberPath = "Login";
            cBoxLogin.ItemsSource = ppDataBaseEntities.GetContext().Users.ToList();

            if (edit)
            {
                tBoxFirstName.Text = client.FirstName;
                tBoxLastName.Text = client.LastName;
                tBoxPatronymic.Text = client.Patronymic;

                cBoxLogin.SelectedItem = client.Users;

                tBoxEmail.Text = client.Email;
                tBoxPhoneNumber.Text = client.PhoneNumbers;

                cBoxAdress.SelectedItem = client.Adresses;
                tBoxNumberAdress.Text = client.AdressNumber.ToString();
                tBoxNumberAportament.Text = client.ApartmentNumber.ToString();
            }
        }
    }
}
