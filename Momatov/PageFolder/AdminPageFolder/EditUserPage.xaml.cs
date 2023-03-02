using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
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

namespace Momatov.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        User userNew = new User();
        User userOld = new User();
        public EditUserPage(User user)
        {
            InitializeComponent();
            userNew.ID = user.ID;
            userNew.Login = user.Login;
            userNew.Password = user.Password;
            userNew.RoleID = user.RoleID;

            userOld = user;

            DataContext = userNew;

            RoleComboBox.ItemsSource = DBEntities.GetContext().UserRole.ToList();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userNew = DBEntities.GetContext().User.FirstOrDefault(x => x.ID == userNew.ID);
                userNew.Password = PasswordTextBox.Text;
                userNew.Login = LoginTextBox.Text;
                userNew.RoleID = Int32.Parse(RoleComboBox.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                NavigationService.Navigate(new ListUserPage());
            }
            catch(Exception ex)
            {
                userNew.Login = userOld.Login;
                userNew.Password = userOld.Password;
                userNew.RoleID = userOld.RoleID;
                MBClass.Error(ex); 
            }
            
        }
    }
}
