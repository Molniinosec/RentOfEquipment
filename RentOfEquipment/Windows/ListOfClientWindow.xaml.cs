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
    /// Логика взаимодействия для ListOfClientWindow.xaml
    /// </summary>
    public partial class ListOfClientWindow : Window
    {
        List<string> ListSort = new List<string> { "По умолчанию", "По фамилии", "По имени", "По гендеру" };

        public ListOfClientWindow()
        {
            InitializeComponent();
            Filter();
            cmbSor.ItemsSource = ListSort;
            cmbSor.SelectedIndex = 0;

        }

        public void Filter()
        {
            List<EF.Client> ListClient = new List<EF.Client>();

            ListClient = ClassHelper.Appdata.Content.Client.Where(i=>i.IsDeleted==false).ToList();

            ListClient = ListClient.Where(i => i.FIO.ToLower().Contains(txtSearch.Text.ToLower())).ToList();


            switch (cmbSor.SelectedIndex)
            {
                case 0:
                    {
                        ListClient = ListClient.OrderBy(i => i.ID).ToList();
                        break;
                    }
                case 1:
                    {
                        ListClient = ListClient.OrderBy(i => i.LastName).ToList();
                        break;
                    }
                case 2:
                    {
                        ListClient = ListClient.OrderBy(i => i.FirstName).ToList();
                        break;
                    }
                case 3:
                    {
                        ListClient = ListClient.OrderBy(i => i.GenderID).ToList();
                        break;
                    }
                default:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvClient.ItemsSource = ListClient;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        
        private void cmbSor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddEmployeeInList_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            Filter();
        }

        private void lvClient_KeyDown(object sender, KeyEventArgs e)
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
                    if (lvClient.SelectedItem is EF.Client)
                    {
                        var empl = lvClient.SelectedItem as EF.Client;
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
    }
}
