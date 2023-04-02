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
    /// Логика взаимодействия для ApplicationPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        Users users;
        public ApplicationPage(Users user)
        {
            InitializeComponent();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;
            Applications applications = dataGrid.SelectedItems as Applications;
            try
            {
                ppDataBaseEntities.GetContext().Applications.Remove(applications);
                ppDataBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены! ");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            dataGrid.ItemsSource = GetApplications();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ApplicationsAddEditPage());

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new ApplicationsAddEditPage(dataGrid.SelectedItem as Applications));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetApplications();
        }

        private void cBoxStatusSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.ItemsSource = GetApplications();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cBoxStatusSearch.SelectedItem = null;
            tBoxSearch.Text = null;
        }


        public List<Applications> GetApplications()
        {
            List<Applications> apps = ppDataBaseEntities.GetContext().Applications.ToList();
            if (users.Role_ID == 2)
            {
                apps = apps.Where(a => a.Complaints.Clients != null && a.Complaints.Clients.Users_ID == users.ID).ToList();
            }
                if (tBoxSearch.Text.Length > 0) {
                string str = tBoxSearch.Text.ToLower();
                apps = apps.Where(a => a.Executors.FirstName.ToLower().Contains(str) ||
                a.Executors.LastName.ToLower().Contains(str) ||
                a.Executors.Patronymic.ToLower().Contains(str) ||

                a.Complaints.Clients.FirstName.ToLower().Contains(str) ||
                a.Complaints.Clients.LastName.ToLower().Contains(str) ||
                a.Complaints.Clients.Patronymic.ToLower().Contains(str))
                    .ToList();
            }
            if (cBoxStatusSearch.SelectedValue != null) { 
                int statID = Convert.ToInt32(cBoxStatusSearch.SelectedValue);
                apps = apps.Where(a=> a.Status_ID == statID).ToList();
            }
            return apps;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = GetApplications();


            cBoxStatus.SelectedValuePath = "ID";
            cBoxStatus.DisplayMemberPath = "Name";
            cBoxStatus.ItemsSource = ppDataBaseEntities.GetContext().Statuses.ToList();

            cBoxExecuter.SelectedValuePath = "ID";
            cBoxExecuter.DisplayMemberPath = "LastName";
            cBoxExecuter.ItemsSource = ppDataBaseEntities.GetContext().Executors.ToList();
        
            if(users.Role_ID == 2)
            {
                btnDel.Visibility = Visibility.Collapsed;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                cBoxStatus.Visibility = Visibility.Collapsed;
                cBoxExecuter.Visibility = Visibility.Collapsed;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            if (((Applications)dataGrid.SelectedItem).Statuses != null)
                cBoxStatus.SelectedItem = ((Applications)dataGrid.SelectedItem).Statuses;
            else
                cBoxStatus.SelectedItem = null;

            if (((Applications)dataGrid.SelectedItem).Executors != null)
                cBoxExecuter.SelectedItem = ((Applications)dataGrid.SelectedItem).Executors;
            else
                cBoxExecuter.SelectedItem = null;

        }
    }
}
