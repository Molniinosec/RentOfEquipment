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
    /// Логика взаимодействия для ListOfRent.xaml
    /// </summary>
    public partial class ListOfRent : Window
    {
        List<string> ListSort = new List<string> { "По умолчанию", "По Клиенту", "По Оборудованию", "По Сотруднику", "По Цене" };
        public ListOfRent()
        {
            InitializeComponent();
            Filter();
        }

        public List<EF.ClientProduct> TotalCost(List<EF.ClientProduct> list) 
        {
            foreach (EF.ClientProduct cp in list) 
            {
                TimeSpan time =cp.RentEndDate - cp.RentStartDate;
                cp.Product.Cost =Convert.ToDecimal((Convert.ToDouble(cp.Product.Cost)*0.05)*time.Days);
            }
            return list;
        }
        public void Filter()
        {
            List<EF.ClientProduct> listRent = new List<EF.ClientProduct>();

            listRent = ClassHelper.Appdata.Content.ClientProduct.ToList();

            listRent = TotalCost(listRent);
            listRent = listRent.Where(i => i.Client.LastName.ToLower().Contains(txtSearch.Text.ToLower()) 
            || i.Product.Name.ToLower().Contains(txtSearch.Text.ToLower()) 
            || i.Employee.SecondName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            lvRent.ItemsSource = listRent;
        }
        private void cmbSor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void lvClient_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
