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
            ListStaffListBox.ItemsSource = DBEntities.GetContext()
                .Staff.ToList().OrderBy(s => s.LastName);
        }

        async private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string sr = SearchTextBox.Text.ToLower();

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
