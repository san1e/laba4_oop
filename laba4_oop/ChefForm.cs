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
    public partial class ChefForm : Form
    {
        public ChefDTO ChefDTO { get; private set; }

        public ChefForm(Chef chef = null)
        {
            InitializeComponent();

            if (chef != null)
            {
                ChefDTO = new ChefDTO
                {
                    FirstName = chef.FirstName,
                    LastName = chef.LastName
                };

                firstNameTextBox.Text = ChefDTO.FirstName;
                lastNameTextBox.Text = ChefDTO.LastName;
            }
            else
            {
                ChefDTO = new ChefDTO();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            ChefDTO.FirstName = firstNameTextBox.Text;
            ChefDTO.LastName = lastNameTextBox.Text;

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
                    DialogResult = DialogResult.OK;
                    e.Cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
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
            if (lastNameTextBox.Text.Length < 2)
            {
                errorProvider1.SetError(lastNameTextBox, "Прізвище кухаря повинно бути щонайменше 2 символи");
            }
            else
            {
                errorProvider1.SetError(lastNameTextBox, "");
            }
        }

        private void ChefForm_Load(object sender, EventArgs e)
        {

        }

        // ... (інший код ChefForm)
    }
}