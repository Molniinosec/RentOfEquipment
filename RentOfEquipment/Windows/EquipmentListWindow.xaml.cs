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
        public EquipmentListWindow()
        {
            InitializeComponent();
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;
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
    }
}
