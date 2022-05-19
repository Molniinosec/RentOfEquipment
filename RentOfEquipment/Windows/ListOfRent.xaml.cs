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
        EF.Employee globalEmpl;
        List<string> ListSort = new List<string> { "По умолчанию", "По Клиенту", "По Сотруднику","По Оборудованию" };
        public ListOfRent(EF.Employee thisEmpl)
        {
            InitializeComponent();
            globalEmpl = thisEmpl;
            List<EF.ClientProduct> listRent = new List<EF.ClientProduct>();
            listRent = ClassHelper.Appdata.Content.ClientProduct.ToList();
            foreach (EF.ClientProduct cp in listRent)
            {
                cp.Product.Cost = LibbraryCalculate.ClientsServiceClass.TotalCost(cp.RentStartDate,cp.RentEndDate,Convert.ToDouble(cp.Product.Cost));

            }
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;

        }

        //public List<EF.ClientProduct> TotalCost(List<EF.ClientProduct> list)
        //{
        //    foreach (EF.ClientProduct cp in list)
        //    {
        //        TimeSpan time = (cp.RentEndDate - cp.RentStartDate);
        //        int timer = Convert.ToInt32(time.TotalDays);
        //        cp.Product.Cost = Math.Round(Convert.ToDecimal((Convert.ToDouble(cp.Product.Cost) * 0.05) * (timer + 1)), 2);
        //    }
        //    return list;
        //}
        public void Filter()
        {
            List<EF.ClientProduct> listRent = new List<EF.ClientProduct>();

            listRent = ClassHelper.Appdata.Content.ClientProduct.ToList();


            listRent = listRent.Where(i => i.Client.LastName.ToLower().Contains(txtSearch.Text.ToLower()) 
            || i.Product.Name.ToLower().Contains(txtSearch.Text.ToLower()) 
            || i.Employee.SecondName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSor.SelectedIndex)
            {
                case 0:
                    {
                        listRent = listRent.OrderBy(i => i.ID).ToList();
                        break;
                    }
                case 1:
                    {
                        listRent = listRent.OrderBy(i => i.Client.LastName).ToList();
                        break;
                    }
                case 2:
                    {
                        listRent = listRent.OrderBy(i => i.Employee.SecondName).ToList();
                        break;
                    }
                case 3:
                    {
                        listRent = listRent.OrderBy(i => i.Product.Name).ToList();
                        break;
                    }
                default:
                    listRent = listRent.OrderBy(i => i.ID).ToList();
                    break;
            }
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


    }
}
