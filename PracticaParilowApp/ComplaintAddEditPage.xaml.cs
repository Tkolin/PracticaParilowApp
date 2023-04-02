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
    /// Логика взаимодействия для ComplaintAddEditPage.xaml
    /// </summary>
    public partial class ComplaintAddEditPage : Page
    {
        Complaints complaint;
        bool edit;
        Clients user = null;
        public ComplaintAddEditPage()
        {
            InitializeComponent();
            this.complaint = new Complaints();
            this.edit = false;
        }
        public ComplaintAddEditPage(Complaints complaint)
        {
            InitializeComponent();
            this.complaint = complaint;
            this.edit = true;
        }
        public ComplaintAddEditPage(Users user)
        {
            InitializeComponent();
            if(user.Role_ID != 1 || user.Role_ID != 3)
            this.user = ppDataBaseEntities.GetContext().Clients.Where(c=>c.Users_ID==user.ID).First();
            this.edit = true;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (tBoxMessage.Text.Length < 0 || cBoxClientLogin.SelectedItem == null)
                return;

            complaint.Clients = cBoxClientLogin.SelectedItem as Clients;
            complaint.Message = tBoxMessage.Text;
            complaint.Date = DateTime.Now;

            if (!edit)
            {
                try
                {
                    MessageBox.Show("Данные добавлены!");
                    ppDataBaseEntities.GetContext().Complaints.Add(complaint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {

                ppDataBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cBoxClientLogin.SelectedValuePath = "ID";
            cBoxClientLogin.DisplayMemberPath = "LastName";
            cBoxClientLogin.ItemsSource = ppDataBaseEntities.GetContext().Clients.ToList();

            if (edit)
            {
                if (user != null)
                {
                    cBoxClientLogin.IsEditable = false;
                    cBoxClientLogin.SelectedItem = user;
                }
                else if (complaint.Clients != null)
                    cBoxClientLogin.SelectedItem = complaint.Clients.Users;
                tBoxNumberComplation.Text = complaint.ID.ToString();
                tBoxMessage.Text = complaint.Message;
            }
        }
    }
}
