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
    /// Логика взаимодействия для EquipmentListWindow.xaml
    /// </summary>
    public partial class EquipmentListWindow : Window
    {
        List<string> ListSort = new List<string> { "По ID","По названию", "По цене", "По категории" };
        bool isSelected = false;
        public EquipmentListWindow()
        {
            InitializeComponent();
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;
        }
        public EquipmentListWindow(bool select)
        {
            InitializeComponent();
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;
            isSelected = true;
        }


        public void Filter()
        {
            List<EF.Product> products = new List<EF.Product>();

            products = ClassHelper.Appdata.Content.Product.Where(i => i.IsDeleted == false).ToList();

            products = products.Where(i => i.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            switch (cmbSor.SelectedIndex)
            {
                case 0:
                    {
                        products = products.OrderBy(i => i.ID).ToList();
                        break;
                    }
                case 1:
                    {
                        products = products.OrderBy(i => i.Name).ToList();
                        break;
                    }
                case 2:
                    {
                        products = products.OrderBy(i => i.Cost).ToList();
                        break;
                    }
                case 3:
                    {
                        products = products.OrderBy(i => i.Category.Name).ToList();
                        break;
                    }
            
                default:
                    products = products.OrderBy(i => i.ID).ToList();
                    break;
            }
            lvEquipment.ItemsSource = products;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void lvEquipment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show("Вы уверенны, что хотите удалить данную запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    if (lvEquipment.SelectedItem is EF.Product)
                    {
                        var empl = lvEquipment.SelectedItem as EF.Product;
                        empl.IsDeleted = true;
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

        private void lvEquipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (isSelected == true)
            {
                ClassHelper.Helper.EquipmentInfo = lvEquipment.SelectedItem as EF.Product;
                this.Close();
            }
            else
            {
                if (lvEquipment.SelectedItem is EF.Product)
                {
                    var empl = lvEquipment.SelectedItem as EF.Product;

                    AddEmployeeWindow1 addEmployeeWindow1 = new AddEmployeeWindow1(/*empl*/);
                    addEmployeeWindow1.ShowDialog();
                    Filter();
                }
            }
           
        }

        private void btnAddEmployeeInList_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
