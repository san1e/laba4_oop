using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_oop
{
    public partial class DishForm : Form
    {
        public Dish Dish { get; private set; }

        public DishForm(List<Chef> chefs, Dish dish = null)
        {
            InitializeComponent();

            // Заповнення ComboBox для категорій
            categoryComboBox.Items.AddRange(Enum.GetNames(typeof(Category)));
            if (categoryComboBox.Items.Count > 0)
            {
                categoryComboBox.SelectedIndex = 0;
            }

            // Заповнення ComboBox для кухарів
            chefComboBox.Items.AddRange(chefs.ToArray());
            if (chefComboBox.Items.Count > 0)
            {
                chefComboBox.SelectedIndex = 0;
            }

            if (dish != null) // Якщо передана страва, заповнити поля
            {
                Dish = dish;
                nameTextBox.Text = Dish.Name;
                priceTextBox.Text = Dish.Price.ToString();
                cookingTimeTextBox.Text = Dish.CookingTime.ToString();
                categoryComboBox.SelectedIndex = (int)Dish.Category;
                chefComboBox.SelectedItem = Dish.Chef;
            }
            else
            {
                // Проверка на пустой список chefs
                if (chefs.Count > 0)
                {
                    Dish = new Dish("", 0, 0, Category.ХолодніЗакуски, chefs[0]);
                }
                else
                {
                    // Здесь можно создать Dish с null поваром или  
                    // вывести сообщение пользователю 
                    Dish = new Dish("", 0, 0, Category.ХолодніЗакуски, null);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Dish.Name = nameTextBox.Text;
                Dish.Price = int.Parse(priceTextBox.Text);
                Dish.CookingTime = int.Parse(cookingTimeTextBox.Text);
                Dish.Category = (Category)categoryComboBox.SelectedIndex;
                Dish.Chef = (Chef)chefComboBox.SelectedItem;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Валідація введених даних
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProvider1.SetError(nameTextBox, "Введіть назву страви");
                return false;
            }
            else
            {
                errorProvider1.SetError(nameTextBox, "");
            }

            if (!int.TryParse(priceTextBox.Text, out int price) || price <= 0)
            {
                errorProvider1.SetError(priceTextBox, "Введіть коректну ціну страви");
                return false;
            }
            else
            {
                errorProvider1.SetError(priceTextBox, "");
            }

            if (!int.TryParse(cookingTimeTextBox.Text, out int cookingTime) || cookingTime <= 0)
            {
                errorProvider1.SetError(cookingTimeTextBox, "Введіть коректний час приготування");
                return false;
            }
            else
            {
                errorProvider1.SetError(cookingTimeTextBox, "");
            }

            return true;
        }
    }
}