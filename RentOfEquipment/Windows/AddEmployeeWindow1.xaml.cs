using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEmployeeWindow1.xaml
    /// </summary>
    public partial class AddEmployeeWindow1 : Window
    {
        private bool isEdit;
        EF.Employee EditEmployee = new EF.Employee();
        string pathPhoto = null;
        public AddEmployeeWindow1()
        {
            InitializeComponent();
            cmbRole.ItemsSource = ClassHelper.Appdata.Content.Role.ToList();
            cmbRole.DisplayMemberPath = "Role1";
            cmbRole.SelectedIndex = 0;
            cmbGender.ItemsSource = ClassHelper.Appdata.Content.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";
            cmbGender.SelectedIndex = 0;
            isEdit = false;
        }
        public AddEmployeeWindow1(EF.Employee employee)
        {
            InitializeComponent();
            cmbRole.ItemsSource = ClassHelper.Appdata.Content.Role.ToList();
            cmbRole.DisplayMemberPath = "Role1";
            cmbGender.ItemsSource = ClassHelper.Appdata.Content.Gender.ToList();
            cmbGender.DisplayMemberPath = "Gender1";

            txtLName.Text= employee.SecondName;
            txtFName.Text= employee.FirstName;
            txtMName.Text= employee.Patronymic;
            txtPhone.Text= employee.Phone;
            cmbRole.SelectedIndex = employee.IDRole - 1;
            cmbGender.SelectedIndex = employee.IDGender - 1;
            txtLogin.Text = employee.Login;
            txtPassword.Password= employee.Password;
            txtRepeatPassword.Password = employee.Password;
            if (employee.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(employee.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    PhotoEmployee.Source = bitmapImage;
                }
            }
          

            tbTittle.Text = "Изменение сотрудника";
            btnAdd.Content = "Изменить";
            isEdit = true;
            EditEmployee = employee;
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
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Поле Логин не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Поле Пароль не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtPassword.Password!=txtRepeatPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtPhone.Text.Length > 12)
            {
                MessageBox.Show("Превышен лимит символов в поле Телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(Int32.TryParse(txtPhone.Text, out int res))
            {
                MessageBox.Show("Нелья вводить символы в поле Телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resLogin = ClassHelper.Appdata.Content.Employee.ToList().
              Where(i => i.Login==txtLogin.Text && i.Login!=EditEmployee.Login).FirstOrDefault();
            if (resLogin!=null)
            {
                MessageBox.Show("Такой логин уже есть, введите другой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Изменение

            if (isEdit==true)
            {
                var resClick = MessageBox.Show("Вы уверенны?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

               EditEmployee.SecondName = txtLName.Text;
               EditEmployee.FirstName = txtFName.Text;
               EditEmployee.Patronymic = txtMName.Text;
               EditEmployee.Phone = txtPhone.Text;
               EditEmployee.IDRole = (cmbRole.SelectedItem as EF.Role).ID;
               EditEmployee.IDGender = (cmbGender.SelectedItem as EF.Gender).ID;
               EditEmployee.Login = txtLogin.Text;
               EditEmployee.Password = txtPassword.Password;
                if (pathPhoto != null)
                {
                    EditEmployee.Photo = File.ReadAllBytes(pathPhoto);
                }

                ClassHelper.Appdata.Content.SaveChanges();

                MessageBox.Show("Сотрудник Изменен");
                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Вы уверенны?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                //Добавление сотрудника

                try
                {
                    EF.Employee modelEmployee = new EF.Employee();

                    modelEmployee.SecondName = txtLName.Text;
                    modelEmployee.FirstName = txtFName.Text;
                    modelEmployee.Patronymic = txtMName.Text;
                    modelEmployee.Phone = txtPhone.Text;
                    modelEmployee.IDRole = (cmbRole.SelectedItem as EF.Role).ID;
                    modelEmployee.IDGender = (cmbGender.SelectedItem as EF.Gender).ID;
                    modelEmployee.Login = txtLogin.Text;
                    modelEmployee.Password = txtPassword.Password;
                    if (pathPhoto != null)
                    {
                        EditEmployee.Photo = File.ReadAllBytes(pathPhoto);
                    }
                    ClassHelper.Appdata.Content.Employee.Add(modelEmployee);
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

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890+-".IndexOf(e.Text) < 0;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                PhotoEmployee.Source =new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }
    }
}
