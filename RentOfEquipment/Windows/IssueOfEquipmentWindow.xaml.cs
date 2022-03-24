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
    /// Логика взаимодействия для IssueOfEquipmentWindow.xaml
    /// </summary>
    public partial class IssueOfEquipmentWindow : Window
    {
        EF.Employee globalEmpl;
        public IssueOfEquipmentWindow(EF.Employee thisEmpl)
        {
            InitializeComponent();
            globalEmpl = thisEmpl;
        }

        private void bthAddClient_Click(object sender, RoutedEventArgs e)
        {
            bool select=true;
            ListOfClientWindow listOfClientWindow = new ListOfClientWindow(select);
            listOfClientWindow.ShowDialog();
            tbIDClient.Text = ClassHelper.Helper.ClientInfo.ID.ToString();
            tbClientFIO.Text = ClassHelper.Helper.ClientInfo.LastName.ToString()+" " + ClassHelper.Helper.ClientInfo.FirstName.ToString()+" " + ClassHelper.Helper.ClientInfo.MiddleName.ToString();
        }

        private void bthAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            bool select = true;
            EquipmentListWindow equipmentListWindow = new EquipmentListWindow(select);
            equipmentListWindow.ShowDialog();
            tbIDEquipment.Text = ClassHelper.Helper.EquipmentInfo.ID.ToString();
            tbEquipmentTittle.Text = ClassHelper.Helper.EquipmentInfo.Name.ToString();
        }
    }
}
