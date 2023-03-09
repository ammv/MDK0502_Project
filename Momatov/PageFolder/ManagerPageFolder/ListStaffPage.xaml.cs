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

                await Task.Run(() =>
                {
                    ListStaffListBox.Dispatcher.Invoke(() =>
                    {
                        if (!string.IsNullOrEmpty(sr))
                        {
                            ListStaffListBox.ItemsSource = DBEntities.GetContext()
                            .Staff.Where(s => sr.StartsWith(s.FirstName) ||
                                sr.StartsWith(s.MiddleName) ||
                                sr.StartsWith(s.LastName) ||
                                sr.StartsWith(s.Phone))
                            .ToList()
                            .OrderBy(s => s.LastName);
                        }
                        else
                        {
                            ListStaffListBox.ItemsSource = DBEntities.GetContext()
                                .Staff.ToList().OrderBy(s => s.LastName);
                        }
                    });
                    
                });

                

                
                
            }
            catch(Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private async Task<IOrderedEnumerable<Staff>> GetFullStaffListAsync()
        {
            return await Task.Run(() => DBEntities.GetContext()
                .Staff.ToList().OrderBy(s => s.LastName));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Загрузка списка сотрудников...";
            var result = await GetFullStaffListAsync();
            ListStaffListBox.ItemsSource = result;
            Title = $"Список сотрудников\nНайдено: {result.Count()}";
        }
    }

    public class StringToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if(string.IsNullOrEmpty(s)) return null;
            return ImageClass.ConvertBase64StringToBitmapImage((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
