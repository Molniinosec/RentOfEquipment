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
    /// Логика взаимодействия для AddEquipmentWindow.xaml
    /// </summary>
    public partial class AddEquipmentWindow : Window
    {
        private bool isEdit;
        EF.Product EditProduct = new EF.Product();
        public AddEquipmentWindow()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = ClassHelper.Appdata.Content.Category.ToList();
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedIndex = 0;
        }
        public AddEquipmentWindow(EF.Product prod)
        {
            InitializeComponent();
            cmbCategory.ItemsSource = ClassHelper.Appdata.Content.Category.ToList();
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedIndex = 0;
            isEdit = true;
            EditProduct = prod;
            txtName.Text = prod.Name;
            txtDescription.Text = prod.Description;
            txtCost.Text = prod.Cost.ToString();
            cmbCategory.SelectedItem = prod.Category;
            dataWaranty.SelectedDate = prod.Warranty;
            tbTittle.Text = "Изменение оборудования";
            btnAdd.Content = "Изменение";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (isEdit == true)
                {
                    var resClick = MessageBox.Show("Вы уверенны?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (resClick == MessageBoxResult.No)
                    {
                        return;
                    }

                    EditProduct.Name = txtName.Text;
                    EditProduct.Description = txtDescription.Text;
                    EditProduct.Cost = Convert.ToDecimal(txtCost.Text);
                    EditProduct.IDCategory = (cmbCategory.SelectedItem as EF.Product).ID;
                    EditProduct.Warranty = dataWaranty.DisplayDate;



                    ClassHelper.Appdata.Content.SaveChanges();

                    MessageBox.Show("Сотрудник Изменен");
                    this.Close();
                }
                else
                {
                    EF.Product modelEquipment = new EF.Product();

                    modelEquipment.Name = txtName.Text;
                    modelEquipment.Description = txtDescription.Text;
                    modelEquipment.Cost = Convert.ToDecimal(txtCost.Text);
                    modelEquipment.IDCategory = (cmbCategory.SelectedItem as EF.Category).ID;
                    modelEquipment.Warranty = dataWaranty.DisplayDate;
                    modelEquipment.IsDeleted = false;

                    ClassHelper.Appdata.Content.Product.Add(modelEquipment);
                    ClassHelper.Appdata.Content.SaveChanges();

                    MessageBox.Show("Оборудование добавлено");
                    this.Close();
                }
               
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message).ToString();
            }
        }
    }
}
