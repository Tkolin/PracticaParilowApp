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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            try
            {
                ppDataBaseEntities.GetContext().Clients.Remove(dataGrid.SelectedItem as Clients);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientAddEditPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new ClientAddEditPage(dataGrid.SelectedItem as Clients));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetClients();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBoxAdressSearch.SelectedValuePath = "ID";
            cBoxAdressSearch.DisplayMemberPath = "Name";
            cBoxAdressSearch.ItemsSource = ppDataBaseEntities.GetContext().Adresses.ToList();

            dataGrid.ItemsSource = GetClients();
        }

        private void cBoxAdressSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetClients();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tBoxSearch.Text = null;
            cBoxAdressSearch.SelectedItem = null;
        }
        public List<Clients> GetClients()
        {
            List<Clients> apps = ppDataBaseEntities.GetContext().Clients.ToList();

            if (tBoxSearch.Text.Length > 0)
            {
                string str = tBoxSearch.Text.ToLower();
                apps = apps.Where(a => 
                a.FirstName.ToLower().Contains(str) ||
                a.LastName.ToLower().Contains(str) ||
                a.Patronymic.ToLower().Contains(str))
                    .ToList();
            }
            if (cBoxAdressSearch.SelectedValue != null)
            {
                int statID = Convert.ToInt32(cBoxAdressSearch.SelectedValue);
                apps = apps.Where(a => a.AdressName_ID == statID).ToList();
            }
            return apps;
        }
    }
}
