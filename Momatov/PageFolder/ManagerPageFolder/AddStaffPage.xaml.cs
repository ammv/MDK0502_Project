﻿using Momatov.ClassFolder;
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

namespace Momatov.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStaffPage.xaml
    /// </summary>
    public partial class AddStaffPage : Page
    {
        Staff staff = new Staff();
        User user = new User();
        public AddStaffPage()
        {
            InitializeComponent();
            RoleComboBox.ItemsSource = DBEntities.GetContext().UserRole;
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ValidateUser() && ValidateStaff())
                {
                    AddUser();
                    AddStaff();
                }
                

                
            }
            catch(Exception ex)
            {
                MBClass.Error(ex)
            }
        }

        private void AddUser()
        {
            throw new NotImplementedException();
        }

        private bool ValidateStaff()
        {
            throw new NotImplementedException();
        }

        private void AddStaff()
        {
            throw new NotImplementedException();
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

        }
    }
}