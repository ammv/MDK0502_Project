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

        //private IEnumerable<Staff> ParallelSearchStaff(string text)
        //{
        //    List<Staff> staff = new List<Staff>();
        //    Parallel.ForEach(DBEntities.GetContext().Staff, s => 
        //        { 
        //            if(s.FirstName.StartsWith(text, StringComparison.OrdinalIgnoreCase) ||
        //                s.MiddleName.StartsWith(text, StringComparison.OrdinalIgnoreCase) ||
        //                s.LastName.StartsWith(text, StringComparison.OrdinalIgnoreCase) ||
        //                s.Phone.StartsWith(text))
        //            {
        //                staff.Add(s);
        //            }
        //        });
        //    return staff.OrderBy(s => s.LastName);
        //}

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string sr = SearchTextBox.Text.ToLower().Trim();

                if (!string.IsNullOrEmpty(sr))
                {
                    ListStaffListBox.ItemsSource = await SearchStaffAsync(sr);
                }
                else
                {
                    ListStaffListBox.ItemsSource = await Task.Run(() => DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.LastName));
                }
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
