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
        private bool isEdit;
        EF.Client EditClient = new EF.Client();
        public AddClientWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = ClassHelper.Appdata.Content.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";
            cmbGender.SelectedIndex = 0;
        }
        public AddClientWindow(EF.Client client)
        {
            InitializeComponent();
            cmbGender.ItemsSource = ClassHelper.Appdata.Content.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";
            cmbGender.SelectedIndex = 0;

            txtFName.Text = client.FirstName;
            txtLName.Text = client.LastName;
            txtMName.Text = client.MiddleName;
            cmbGender.SelectedIndex = client.GenderID - 1;
            dpBirthday.SelectedDate = client.Birthday;
            txtMName.Text = client.Email;
            txtPhone.Text = client.Phone;
            txtSerialPassport.Text = client.Passport.Serial;
            txtNumberPassport.Text = client.Passport.Number;
            tbtittle.Text = "Изменение клиента";
            btnAdd.Content = "Изменить клиента";
            isEdit = true;
            EditClient = client;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
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
            if (string.IsNullOrWhiteSpace(txtMail.Text))
            {
                MessageBox.Show("Поле Почта не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSerialPassport.Text))
            {
                MessageBox.Show("Поле Серия паспорта не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNumberPassport.Text))
            {
                MessageBox.Show("Поле Номер паспорта не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(dpBirthday.Text))
            {
                MessageBox.Show("Поле Дата рождения не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверенны?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (resClick == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                //Добавление паспорта в базу
                EF.Passport modelPassport = new EF.Passport();
                modelPassport.Number = txtNumberPassport.Text;
                modelPassport.Serial = txtSerialPassport.Text;
                ClassHelper.Appdata.Content.Passport.Add(modelPassport);
                ClassHelper.Appdata.Content.SaveChanges();
                var passport = ClassHelper.Appdata.Content.Passport.Where(i => i.Serial == txtSerialPassport.Text && i.Number == txtNumberPassport.Text).FirstOrDefault();

                //Добавления клиента в базу
                EF.Client modelClient = new EF.Client();
                modelClient.LastName = txtLName.Text;
                modelClient.FirstName = txtFName.Text;
                modelClient.MiddleName = txtMName.Text;
                modelClient.GenderID = (cmbGender.SelectedItem as EF.Gender).ID;
                modelClient.Birthday = dpBirthday.DisplayDate;
                modelClient.Email = txtMail.Text;
                modelClient.Phone = txtPhone.Text;
                if (passport != null)
                {
                    modelClient.PassportID = passport.ID;
                }
                else
                {
                   
                    return;
                }
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
