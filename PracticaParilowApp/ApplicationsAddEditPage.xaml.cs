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
    /// Логика взаимодействия для ApplicationsAddEditPage.xaml
    /// </summary>
    public partial class ApplicationsAddEditPage : Page
    {
        Applications applications;
        Complaints complaints;
        bool edit;
        public ApplicationsAddEditPage()
        {
            InitializeComponent();
            this.applications = new Applications();
            this.edit = false;
        }
        public ApplicationsAddEditPage(Applications applications)
        {
            InitializeComponent();
            this.applications = applications;
            this.edit = true;
        }
        public ApplicationsAddEditPage(Complaints complaints)
        {
            InitializeComponent();
            this.complaints = complaints;
            this.edit = true;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBoxTypeApplications.SelectedValuePath = "ID";
            cBoxTypeApplications.DisplayMemberPath = "Name";
            cBoxTypeApplications.ItemsSource = ppDataBaseEntities.GetContext().TypsApplications.ToList();

            cBoxStatus.SelectedValuePath = "ID";
            cBoxStatus.DisplayMemberPath = "Name";
            cBoxStatus.ItemsSource = ppDataBaseEntities.GetContext().Statuses.ToList();

            cBoxExecutor.SelectedValuePath = "ID";
            cBoxExecutor.DisplayMemberPath = "Name";
            cBoxExecutor.ItemsSource = ppDataBaseEntities.GetContext().Statuses.ToList();

            cBoxClientLogin.SelectedValuePath = "ID";
            cBoxClientLogin.DisplayMemberPath = "LastName";
            cBoxClientLogin.ItemsSource = ppDataBaseEntities.GetContext().Clients.ToList();

            if (edit)
            {
                if(complaints ==)
            }
        }
    }
}
