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
    /// Логика взаимодействия для ComplaintPage.xaml
    /// </summary>
    public partial class ComplaintPage : Page
    {
        Users user;
        public ComplaintPage()
        {
            InitializeComponent();
        }
        public ComplaintPage(Users users)
        {
            InitializeComponent();
            this.user = users;
        }

        private void btnAddApplications_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new ApplicationsAddEditPage(dataGrid.SelectedItem as Complaints));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new ComplaintAddEditPage(dataGrid.SelectedItem as Complaints));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComplaintAddEditPage());

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            try
            {
                ppDataBaseEntities.GetContext().Complaints.Remove(dataGrid.SelectedItem as Complaints); 
                ppDataBaseEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tBoxSearch.Text = null;
            cBoxAdressSearch.SelectedItem = null;
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetComplaints();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBoxAdressSearch.SelectedValuePath = "ID";
            cBoxAdressSearch.DisplayMemberPath = "Name";
            cBoxAdressSearch.ItemsSource = ppDataBaseEntities.GetContext().Adresses.ToList();

            dataGrid.ItemsSource = GetComplaints();
        }

        private void cBoxAdressSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetComplaints();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public List<Complaints> GetComplaints()
        {
            List<Complaints> apps = ppDataBaseEntities.GetContext().Complaints.ToList();

            if (tBoxSearch.Text.Length > 0)
            {
                string str = tBoxSearch.Text.ToLower();
                apps = apps.Where(a => a.Clients != null &&(
                a.Clients.FirstName.ToLower().Contains(str) ||
                a.Clients.LastName.ToLower().Contains(str) ||
                a.Clients.Patronymic.ToLower().Contains(str)))
                    .ToList();
            }
            if (cBoxAdressSearch.SelectedValue != null)
            {
                int statID = Convert.ToInt32(cBoxAdressSearch.SelectedValue);
                apps = apps.Where(a => a.Clients != null&& a.Clients.AdressName_ID == statID).ToList();
            }
            return apps;
        }
    }
}
