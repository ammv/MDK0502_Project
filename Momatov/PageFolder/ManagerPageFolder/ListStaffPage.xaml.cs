using Momatov.ClassFolder;
using Momatov.ClassFolder.DataFolder;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для ListStaffPage.xaml
    /// </summary>
    public partial class ListStaffPage : Page
    {
        public static BitmapImage noStaffImage = new BitmapImage(new Uri("pack://application:,,,/SourceFolder/no-image.png"));
        public ListStaffPage()
        {
            InitializeComponent();
        }

        private IOrderedEnumerable<Staff> SearchStaff(string text)
        {
            return DBEntities.GetContext()
                        .Staff.Where(s => s.FirstName.ToLower().StartsWith(text) ||
                            s.MiddleName.ToLower().StartsWith(text) ||
                            s.LastName.ToLower().StartsWith(text) ||
                            s.Phone.StartsWith(text))
                        .ToList()
                        .OrderBy(s => s.LastName);
        }

        private async Task<IOrderedEnumerable<Staff>> SearchStaffAsync(string text)
        {
            return await Task.Run(() => SearchStaff(text));
        }

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string sr = SearchTextBox.Text.ToLower().Trim();

                Title = "Поиск сотрудника...";
                IOrderedEnumerable<Staff> result;

                if (!string.IsNullOrEmpty(sr))
                {
                    result = await SearchStaffAsync(sr);
                    
                }
                else
                {
                    result = await GetStaffListAsync();
                }

                Title = $"Список сотрудников\nНайдено: {result.Count()}";
                ListStaffListBox.ItemsSource = result;
            }
            catch(Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private async Task<IOrderedEnumerable<Staff>> GetStaffListAsync()
        {
            return await Task.Run(() => DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.LastName));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Загрузка списка сотрудников...";
            var result = await GetStaffListAsync();
            ListStaffListBox.ItemsSource = result;
            Title = $"Список сотрудников\nНайдено: {result.Count()}";
        }
    }

    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if(string.IsNullOrEmpty(s)) return ListStaffPage.noStaffImage;
            return ImageClass.ConvertBase64StringToBitmapImage((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
