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
using System.Windows.Shapes;

namespace RentOfEquipment.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListOfEmplyeeWindow.xaml
    /// </summary>
    public partial class ListOfEmplyeeWindow : Window
    {
        List<string> ListSort = new List<string> { "По умолчанию", "По фамилии", "По имени", "По должности","По гендеру" };
        public ListOfEmplyeeWindow()
        {
            InitializeComponent();
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;
        }

        private void Filter()
        {
            List<EF.Employee> ListEmployee = new List<EF.Employee>();

            ListEmployee= ClassHelper.Appdata.Content.Employee.Where(i=>i.IsDeleted==false).ToList();

            //Поиск

            ListEmployee = ListEmployee.Where(i => i.SecondName.ToLower().Contains(txtSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                || i.FIO.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            //Сортировка

            switch (cmbSor.SelectedIndex)
            {
                case 0:
                    {
                        ListEmployee = ListEmployee.OrderBy(i => i.ID).ToList();
                        break;
                    }
                case 1:
                    {
                        ListEmployee = ListEmployee.OrderBy(i => i.SecondName).ToList();
                        break;
                    }
                case 2:
                    {
                        ListEmployee = ListEmployee.OrderBy(i => i.FirstName).ToList();
                        break;
                    }
                case 3:
                    {
                        ListEmployee = ListEmployee.OrderBy(i => i.IDRole).ToList();
                        break;
                    }
                case 4:
                    {
                        ListEmployee = ListEmployee.OrderBy(i => i.IDGender).ToList();
                        break;
                    }
                default:
                    ListEmployee = ListEmployee.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvEmployee.ItemsSource = ListEmployee;
        }

        private void btnAddEmployeeInList_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow1 addEmployeeWindow1 = new AddEmployeeWindow1();
            addEmployeeWindow1.ShowDialog();
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        //Удаление
        private void lvEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show("Вы уверенны, что хотите удалить данную запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }            
                try
                {
                    if (lvEmployee.SelectedItem is EF.Employee)
                    {
                        var empl = lvEmployee.SelectedItem as EF.Employee;
                        ClassHelper.Appdata.Content.Employee.Remove(empl);
                        ClassHelper.Appdata.Content.SaveChanges();
                        MessageBox.Show("Удалено", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void lvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvEmployee.SelectedItem is EF.Employee)
            {
                var empl = lvEmployee.SelectedItem as EF.Employee;

                AddEmployeeWindow1 addEmployeeWindow1 = new AddEmployeeWindow1(empl);
                addEmployeeWindow1.ShowDialog();
                Filter();
            }
        }
    }
}
