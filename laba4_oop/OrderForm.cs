using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4_oop
{
    public partial class OrderForm : Form
    {
        private Order _order;
        private string _filePath = "order.txt"; // Шлях до файлу для збереження
        private List<Chef> _chefs = new List<Chef>(); // Список кухарів

        public OrderForm()
        {
            InitializeComponent();
            _order = new Order("CafeName", DateTime.Now);
            // Завантаження даних з файлу при запуску
            LoadOrder();
            LoadChefs(); // Завантаження списку кухарів

            // Ініціалізація елементів керування
            UpdateDishList(); // Теперь _order не null
            cafeNameTextBox.Text = _order.CafeName;
            orderDateTimePicker.Value = _order.OrderDate;

            // Заповнення ComboBox для кухарів
            UpdateChefComboBox();
        }

        private void UpdateDishList()
        {
            dishesListBox.Items.Clear();
            foreach (var dish in _order.Dishes)
            {
                dishesListBox.Items.Add($"{dish.Name} ({dish.Price} грн)");
            }
        }

        private void addDishButton_Click(object sender, EventArgs e)
        {
            DishForm dishForm = new DishForm(_chefs); // Передача списку кухарів
            if (dishForm.ShowDialog() == DialogResult.OK)
            {
                _order.AddDish(dishForm.Dish);
                UpdateDishList();
            }
        }

        private void editDishButton_Click(object sender, EventArgs e)
        {
            if (dishesListBox.SelectedIndex != -1)
            {
                Dish selectedDish = _order.Dishes[dishesListBox.SelectedIndex];
                DishForm dishForm = new DishForm(_chefs, selectedDish); // Передача списку кухарів та страви
                if (dishForm.ShowDialog() == DialogResult.OK)
                {
                    _order.Dishes[dishesListBox.SelectedIndex] = dishForm.Dish;
                    UpdateDishList();
                }
            }
        }

        private void addChefButton_Click(object sender, EventArgs e)
        {
            ChefForm chefForm = new ChefForm();
            if (chefForm.ShowDialog() == DialogResult.OK)
            {
                _chefs.Add(chefForm.Chef);
                UpdateChefComboBox();
            }
        }

        private void editChefButton_Click(object sender, EventArgs e)
        {
            if (chefComboBox.SelectedIndex != -1)
            {
                Chef selectedChef = (Chef)chefComboBox.SelectedItem;
                ChefForm chefForm = new ChefForm(selectedChef); // Открываем ChefForm с выбранным поваром
                if (chefForm.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем данные выбранного повара
                    int chefIndex = _chefs.IndexOf(selectedChef); // Находим индекс повара
                    _chefs[chefIndex] = chefForm.Chef; // Обновляем данные

                    UpdateChefComboBox(); // Обновление ComboBoxes в DishForm
                }
            }
            else
            {
                MessageBox.Show("Не обрано повара для редагування!");
            }
        }

        // Обновление chefComboBox
        private void UpdateChefComboBox()
        {
            chefComboBox.Items.Clear();
            chefComboBox.Items.AddRange(_chefs.ToArray());
            if (chefComboBox.Items.Count > 0)
            {
                chefComboBox.SelectedIndex = 0;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Збереження даних о кафе і даті
            SaveOrder();
        }

        private void OrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Зберегти зміни?", "Завершення", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveOrder();
                    SaveChefs(); // Збереження списку кухарів
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void cafeNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Валідація для назви кафе: мінімальна довжина 3 символів
            if (cafeNameTextBox.Text.Length < 3)
            {
                errorProvider1.SetError(cafeNameTextBox, "Назва кафе повинна бути щонайменше 3 символи");
            }
            else
            {
                errorProvider1.SetError(cafeNameTextBox, "");
                _order.CafeName = cafeNameTextBox.Text;
            }
        }

        private void orderDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            _order.OrderDate = orderDateTimePicker.Value;
        }

        // Збереження даних у файл
        private void SaveOrder()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    writer.WriteLine($"CafeName: {_order.CafeName}");
                    writer.WriteLine($"OrderDate: {_order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")}");

                    foreach (Dish dish in _order.Dishes)
                    {
                        writer.WriteLine($"Dish:");
                        writer.WriteLine($"\tName: {dish.Name}");
                        writer.WriteLine($"\tPrice: {dish.Price}");
                        writer.WriteLine($"\tChef: {dish.Chef.FirstName} {dish.Chef.LastName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час збереження!");
            }
        }

        // Завантаження даних з файлу
        private void LoadOrder()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    using (StreamReader reader = new StreamReader(_filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("CafeName:"))
                            {
                                _order.CafeName = line.Substring("CafeName: ".Length);
                            }
                            else if (line.StartsWith("OrderDate:"))
                            {
                                _order.OrderDate = DateTime.Parse(line.Substring("OrderDate: ".Length));
                            }
                            else if (line.StartsWith("Dish:"))
                            {
                                string name = "";
                                int price = 0;
                                Chef chef = null;

                                while ((line = reader.ReadLine()) != null && !line.StartsWith("Dish:"))
                                {
                                    if (line.StartsWith("\tName:"))
                                    {
                                        name = line.Substring("\tName: ".Length);
                                    }
                                    else if (line.StartsWith("\tPrice:"))
                                    {
                                        price = (int)double.Parse(line.Substring("\tPrice: ".Length)); // Преобразование в int
                                    }
                                    else if (line.StartsWith("\tChef:"))
                                    {
                                        string[] chefParts = line.Substring("\tChef: ".Length).Split(' ');
                                        chef = new Chef(chefParts[0], chefParts[1]);
                                    }
                                }

                                // Передача всех параметров в конструктор
                                Dish dish = new Dish(name, price, 0, Category.Невідома, chef); // Передача всех параметров в конструктор

                                _order.AddDish(dish);
                            }
                        }
                    }
                }
                else
                {
                    // Якщо файл не існує, створити нове замовлення
                    _order = new Order("CafeName", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження!");
            }
        }

        // Збереження списку кухарів у файл
        private void SaveChefs()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("chefs.txt"))
                {
                    foreach (var chef in _chefs)
                    {
                        writer.WriteLine($"{chef.FirstName},{chef.LastName}");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        // Завантаження списку кухарів з файлу
        private void LoadChefs()
        {
            try
            {
                if (File.Exists("chefs.txt"))
                {
                    using (StreamReader reader = new StreamReader("chefs.txt"))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            _chefs.Add(new Chef(parts[0], parts[1]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
