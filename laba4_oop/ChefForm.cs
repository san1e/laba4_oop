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
    public partial class ChefForm : Form
    {
        public Chef Chef { get; private set; }

        public ChefForm()
        {
            InitializeComponent();
            Chef = new Chef("", "");
        }

        public ChefForm(Chef chef = null)
        {
            InitializeComponent();

            if (chef != null) // Якщо переданий кухар, заповнити поля
            {
                Chef = chef;
                firstNameTextBox.Text = Chef.FirstName;
                lastNameTextBox.Text = Chef.LastName;
            }
            else
            {
                Chef = new Chef("", "");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Chef.FirstName = firstNameTextBox.Text;
            Chef.LastName = lastNameTextBox.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Зберегти зміни?", "Завершення", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    // Устанавливаем DialogResult.OK для сохранения
                    DialogResult = DialogResult.OK;
                    e.Cancel = false; // Закрываем форму
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = false; // Закрываем форму
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // Не закрываем форму
                }
            }
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Валідація для імені кухаря: мінімальна довжина 2 символів
            if (firstNameTextBox.Text.Length < 2)
            {
                errorProvider1.SetError(firstNameTextBox, "Ім'я кухаря повинно бути щонайменше 2 символи");
            }
            else
            {
                errorProvider1.SetError(firstNameTextBox, "");
            }
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Валідація для прізвища кухаря: мінімальна довжина 2 символів
            if (lastNameTextBox.Text.Length < 2)
            {
                errorProvider1.SetError(lastNameTextBox, "Прізвище кухаря повинно бути щонайменше 2 символи");
            }
            else
            {
                errorProvider1.SetError(lastNameTextBox, "");
            }
        }
    }
}