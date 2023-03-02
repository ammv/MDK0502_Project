using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;

namespace Momatov.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListUserPage.xaml
    /// </summary>
    public partial class ListUserPage : Page
    {
        public ListUserPage()
        {
            InitializeComponent();
            SetListUserDataGridItemsSource();
        }

        public void SetListUserDataGridItemsSource()
        {
            ListUserDataGrid.ItemsSource = DBEntities.GetContext().User
                .ToList()
                .OrderBy(x => x.Login)
                .Where(x => x.Login
                        .Contains(SearchTextBox.Text));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SetListUserDataGridItemsSource();

                //if (ListUserDataGrid.Items.Count == 0)
                //{
                //    MBClass.Error("Пользователь с таким логином не найден");
                //}
            }
            catch(Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private void DeleteUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ListUserDataGrid.SelectedIndex != -1)
                {
                    var user = (User)ListUserDataGrid.Items[ListUserDataGrid.SelectedIndex];
                    if (MBClass.Question($"Вы уверены, что хотите удалить пользователя {user.Login}?"))
                    {
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                        SetListUserDataGridItemsSource();
                        MBClass.Information(user.Login + " удален!");
                    }
                }
                else
                {
                    MBClass.Error("Вы не выбрали пользователя!");
                }
                
                
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private void EditUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(ListUserDataGrid.SelectedItem == null)
            {
                MBClass.Error("Выберите пользователя для редактирования");
            }
            else
            {
                NavigationService.Navigate(new EditUserPage(
                    ListUserDataGrid.SelectedItem as User));
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportClass.ToExcelFile(ListUserDataGrid, "Экспорт пользователей");
        }
    }
}
