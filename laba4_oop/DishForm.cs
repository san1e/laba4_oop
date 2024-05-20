using laba4_dll2.Classes;
using laba4_dll2.DTO;
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
        public DishDTO DishDTO { get; private set; }

        public DishForm(List<Chef> chefs, Dish dish = null)
        {
            InitializeComponent();

            categoryComboBox.Items.AddRange(Enum.GetNames(typeof(Category)));
            categoryComboBox.SelectedIndex = 0;

            chefComboBox.Items.AddRange(chefs.ToArray());
            chefComboBox.SelectedIndex = 0;

            if (dish != null)
            {
                DishDTO = new DishDTO
                {
                    Name = dish.Name,
                    Price = dish.Price,
                    CookingTime = dish.CookingTime,
                    Category = dish.Category,
                    Chef = dish.Chef
                };

                nameTextBox.Text = DishDTO.Name;
                priceTextBox.Text = DishDTO.Price.ToString();
                cookingTimeTextBox.Text = DishDTO.CookingTime.ToString();
                categoryComboBox.SelectedItem = DishDTO.Category;
                chefComboBox.SelectedItem = DishDTO.Chef;
            }
            else
            {
                DishDTO = new DishDTO { Chef = chefs[0] };
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DishDTO.Name = nameTextBox.Text;
                DishDTO.Price = int.Parse(priceTextBox.Text);
                DishDTO.CookingTime = int.Parse(cookingTimeTextBox.Text);

                // Переконайтеся, що SelectedItem не null
                if (categoryComboBox.SelectedItem != null)
                {
                    string categoryName = categoryComboBox.SelectedItem.ToString();
                    // Перетворюємо рядок в значення Category
                    DishDTO.Category = (Category)Enum.Parse(typeof(Category), categoryName);
                }
                else
                {
                    // Обробка ситуації, коли SelectedItem дорівнює null
                    DishDTO.Category = Category.Невідома;
                }

                DishDTO.Chef = (Chef)chefComboBox.SelectedItem;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

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

        private void DishForm_Load(object sender, EventArgs e)
        {

        }
        // ... (інший код DishForm)
    }
}