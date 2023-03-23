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
using Microsoft.Win32;

namespace Momatov.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStaffPage.xaml
    /// </summary>
    public partial class AddStaffPage : Page
    {
        Staff staff = new Staff();
        User user = new User();
        int? workshopID = null;

        public AddStaffPage()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = DBEntities.GetContext().UserRole.ToList();
        }

        private async void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ValidateStaff() && ValidateUser())
                {
                    if(IsWorker())
                    {
                        SetWorkerWorkshop();
                            if(workshopID == 0)
                            {
                                MBClass.Error("Вы не выбрали цех рабочего!");
                                return;
                            }
                    }
                    AddUser();
                    AddStaff();
                    await DBEntities.GetContext().SaveChangesAsync();
                    MBClass.Information("Сотрудник добавлен!");
                } 
            }
            catch(Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private bool IsWorker()
        {
            return (RoleComboBox.SelectedItem as UserRole).Name == "worker";
            
        }

        private void SetWorkerWorkshop()
        {
            new WindowFolder.ManagerWindowFolder.ChoiceWorkshopForWorker((int w) => workshopID = w).ShowDialog();
        }

        private void AddUser()
        {
            user.Login = LoginTextBox.Text;
            user.Password = PasswordTextBox.Text;
            user.RoleID = RoleComboBox.SelectedIndex+1;
            DBEntities.GetContext().User.Add(user);
        }

        private bool ValidateStaff()
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MBClass.Error("Вы не ввели имя!");
                FirstNameTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(MiddleNameTextBox.Text))
            {
                MBClass.Error("Вы не ввели фамилию!");
                PasswordTextBox.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MBClass.Error("Вы не ввели телефон!");
                PasswordTextBox.Focus();
                return false;
            }
            return true;
        }

        private void AddStaff()
        {
            staff.User = user;
            staff.LastName = LastNameTextBox.Text;
            staff.MiddleName = MiddleNameTextBox.Text;
            staff.FirstName = FirstNameTextBox.Text;
            staff.Phone = PhoneTextBox.Text;
            staff.WorkshopID = workshopID;
            DBEntities.GetContext().Staff.Add(staff);
        }

        private bool ValidateUser()
        {
            if(string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MBClass.Error("Вы не ввели логин!");
                LoginTextBox.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MBClass.Error("Вы не ввели пароль!");
                PasswordTextBox.Focus();
                return false;
            }
            if(DBEntities.GetContext().User.FirstOrDefault(x => x.Login == LoginTextBox.Text) != null)
            {
                MBClass.Error("Такой логин уже занят!");
                LoginTextBox.Focus();
                return false;
            }
            if(RoleComboBox.SelectedIndex == -1)
            {
                MBClass.Error("Вы не выбрали роль!");
                RoleComboBox.Focus();
                return false;
            }

            return true;
        }

        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.JPG;*.PNG;*.JPEG)|*.JPG;*.PNG;*.JPEG|All files (*.*)|*.*";
            fileDialog.InitialDirectory = "c:\\";
            fileDialog.Title = "Выберите изображение";
            if(fileDialog.ShowDialog() == true)
            {
                try
                {
                    staff.Photo = ImageClass.ConvertImageToBase64String(fileDialog.FileName);
                    StaffImage.ImageSource = ImageClass.ConvertBase64StringToBitmapImage(staff.Photo);
                }
                catch(Exception ex)
                {
                    MBClass.Error(ex);
                }
                
            }
        }
        private void ClearPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            StaffImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/SourceFolder/no-image.png")); ;
            staff.Photo = null;
        }
    }
}
