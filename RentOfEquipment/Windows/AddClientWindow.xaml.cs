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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = ClassHelper.Appdata.Content.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";
            cmbGender.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверенны?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (resClick == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                EF.Client modelClient = new EF.Client();
                modelClient.LastName = txtLName.Text;
                modelClient.FirstName = txtLName.Text;
                modelClient.MiddleName = txtMName.Text;
                modelClient.GenderID = (cmbGender.SelectedItem as EF.Gender).ID;
                modelClient.Birthday = dpBirthday.DisplayDate;
                modelClient.Email = txtMail.Text;
                modelClient.Phone = txtPhone.Text;
                modelClient.PassportID = Convert.ToInt32(txtPassportID.Text);
                ClassHelper.Appdata.Content.Client.Add(modelClient);
                ClassHelper.Appdata.Content.SaveChanges();

                MessageBox.Show("Сотрудник добавлен");
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message).ToString();
            }

        }
    }
}
