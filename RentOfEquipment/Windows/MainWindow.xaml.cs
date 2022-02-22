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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RentOfEquipment.Windows;

namespace RentOfEquipment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnListEquip_Click(object sender, RoutedEventArgs e)
        {
            EquipmentListWindow equipmentListWindow = new EquipmentListWindow();
            equipmentListWindow.Show();

        }

        private void btnAddEquip_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow();
            addEquipmentWindow.Show();

        }

        private void btnIssueEquip_Click(object sender, RoutedEventArgs e)
        {
            IssueOfEquipmentWindow issueOfEquipmentWindow = new IssueOfEquipmentWindow();
            issueOfEquipmentWindow.Show();
        }

        private void btnAcceptEquip_Click(object sender, RoutedEventArgs e)
        {
            AcceptOfEquipmentWindow acceptOfEquipmentWindow = new AcceptOfEquipmentWindow();
            acceptOfEquipmentWindow.Show();
        }

        private void btnListClient_Click(object sender, RoutedEventArgs e)
        {
            ListOfClientWindow listOfClientWindow = new ListOfClientWindow();
            listOfClientWindow.Show();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.Show();
        }

        private void btnListEmployee_Click(object sender, RoutedEventArgs e)
        {
            ListOfEmplyeeWindow listOfEmplyeeWindow = new ListOfEmplyeeWindow();
            this.Hide();
            listOfEmplyeeWindow.ShowDialog();
            this.Show();
        }
    }
}
