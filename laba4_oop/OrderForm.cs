using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using laba4_oop;
using laba4_dll2.Classes;
using laba4_dll2.DTO;

namespace laba4_oop
{
    public partial class OrderForm : Form
    {
        private List<Order> orders = new List<Order>();
        private string _ordersFilePath = "orders.json";
        private string _dishesFilePath = "dishes.json";
        private OrderDTO _orderDTO;
        private List<Chef> _chefs = new List<Chef>();
        private List<Dish> _dishes = new List<Dish>(); // Список страв

        public OrderForm()
        {
            InitializeComponent();
            _orderDTO = new OrderDTO { CafeName = "CafeName", OrderDate = DateTime.Now };

            LoadOrders();
            LoadChefs();
            LoadDishes();

            UpdateDishList();
            cafeNameTextBox.Text = _orderDTO.CafeName;
            orderDateTimePicker.Value = _orderDTO.OrderDate;

            UpdateChefComboBox();
            UpdateDishComboBox();
            UpdateOrderListBox(); // Оновлення ComboBox для страв
        }
        private void UpdateOrderListBox()
        {
            orderListBox.Items.Clear();
            foreach (var order in orders)
            {
                string orderInfo = order.ToShortString() + "\n"; // Додаємо перехід на новий рядок

                // Додаємо список страв
                foreach (var dish in order.Dishes)
                {
                    orderInfo += $"  - {dish.Name}\n";
                }

                orderListBox.Items.Add(orderInfo);
            }
        }
        private void UpdateDishList()
        {
            dishesListBox.Items.Clear();
            foreach (var dishDTO in _dishes)
            {
                dishesListBox.Items.Add($"{dishDTO.Name} ({dishDTO.Price} грн)");
            }
        }

        private void addDishButton_Click(object sender, EventArgs e)
        {
            DishForm dishForm = new DishForm(_chefs);
            if (dishForm.ShowDialog() == DialogResult.OK)
            {
                _dishes.Add(dishForm.DishDTO.ToDish());
                SaveDishes();
                UpdateDishComboBox();

                _orderDTO.Dishes.Add(dishForm.DishDTO);
                UpdateDishList(); // Додано оновлення dishList
            }
        }

        private void editDishButton_Click(object sender, EventArgs e)
        {
            if (dishesListBox.SelectedIndex != -1)
            {
                DishDTO selectedDishDTO = _orderDTO.Dishes[dishesListBox.SelectedIndex];
                DishForm dishForm = new DishForm(_chefs, selectedDishDTO.ToDish());
                if (dishForm.ShowDialog() == DialogResult.OK)
                {
                    _orderDTO.Dishes[dishesListBox.SelectedIndex] = dishForm.DishDTO;
                    UpdateDishList();

                    // Оновлення страви в списку страв
                    int dishIndex = _dishes.FindIndex(d => d.Name == selectedDishDTO.Name);
                    if (dishIndex != -1)
                    {
                        _dishes[dishIndex] = dishForm.DishDTO.ToDish();
                        SaveDishes(); // Зберігаємо зміни у файл
                        UpdateDishComboBox(); // Оновлюємо ComboBox
                    }
                }
            }
        }

        private void addChefButton_Click(object sender, EventArgs e)
        {
            ChefForm chefForm = new ChefForm();
            if (chefForm.ShowDialog() == DialogResult.OK)
            {
                _chefs.Add(chefForm.ChefDTO.ToChef());
                UpdateChefComboBox();
            }
        }

        private void editChefButton_Click(object sender, EventArgs e)
        {
            if (chefComboBox.SelectedIndex != -1)
            {
                Chef selectedChef = (Chef)chefComboBox.SelectedItem;
                ChefForm chefForm = new ChefForm(selectedChef);
                if (chefForm.ShowDialog() == DialogResult.OK)
                {
                    int chefIndex = _chefs.IndexOf(selectedChef);
                    _chefs[chefIndex] = chefForm.ChefDTO.ToChef();

                    UpdateChefComboBox();
                }
            }
            else
            {
                MessageBox.Show("Не обрано повара для редагування!");
            }
        }

        private void UpdateChefComboBox()
        {
            chefComboBox.Items.Clear();
            chefComboBox.Items.AddRange(_chefs.ToArray());
            if (chefComboBox.Items.Count > 0)
            {
                chefComboBox.SelectedIndex = 0;
            }
        }

        private void UpdateDishComboBox()
        {
            dishComboBox.Items.Clear();
            dishComboBox.Items.AddRange(_dishes.ToArray());
            if (dishComboBox.Items.Count > 0)
            {
                dishComboBox.SelectedIndex = 0;
            }
        }

        // Додавання страви до замовлення
        private void addDishToOrderButton_Click(object sender, EventArgs e)
        {
            if (dishComboBox.SelectedItem != null)
            {
                Dish selectedDish = (Dish)dishComboBox.SelectedItem;
                DishDTO dishDTO = new DishDTO
                {
                    Name = selectedDish.Name,
                    Price = selectedDish.Price,
                    CookingTime = selectedDish.CookingTime,
                    Category = selectedDish.Category,
                    Chef = selectedDish.Chef
                };

                _orderDTO.Dishes.Add(dishDTO);
                UpdateDishList();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
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
                    SaveChefs();
                    SaveDishes();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void cafeNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cafeNameTextBox.Text.Length < 3)
            {
                errorProvider1.SetError(cafeNameTextBox, "Назва кафе повинна бути щонайменше 3 символи");
            }
            else
            {
                errorProvider1.SetError(cafeNameTextBox, "");
                _orderDTO.CafeName = cafeNameTextBox.Text;
            }
        }

        private void orderDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            _orderDTO.OrderDate = orderDateTimePicker.Value;
        }

        // Збереження замовлення
        private void SaveOrder()
        {
            try
            {
                _orderDTO.CafeName = cafeNameTextBox.Text;
                _orderDTO.OrderDate = orderDateTimePicker.Value;

                orders.Add(_orderDTO.ToOrder()); // Додаємо замовлення до списку

                string jsonString = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_ordersFilePath, jsonString);

                // Очищуємо _orderDTO для нового замовлення
                _orderDTO = new OrderDTO { CafeName = "CafeName", OrderDate = DateTime.Now };
                cafeNameTextBox.Text = _orderDTO.CafeName;
                orderDateTimePicker.Value = _orderDTO.OrderDate;
                UpdateDishList();
                UpdateOrderListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час збереження замовлення!");
            }
        }

        // Завантаження замовлень
        private void LoadOrders()
        {
            try
            {
                if (File.Exists(_ordersFilePath))
                {
                    string jsonString = File.ReadAllText(_ordersFilePath);
                    orders = JsonSerializer.Deserialize<List<Order>>(jsonString);

                    // Якщо є замовлення, завантажуємо перше як _orderDTO
                    if (orders.Count > 0)
                    {
                        _orderDTO = new OrderDTO
                        {
                            CafeName = orders[0].CafeName,
                            OrderDate = orders[0].OrderDate,
                            // Завантажуємо страви для кожного замовлення:
                            Dishes = orders[0].Dishes.Select(dish => new DishDTO
                            {
                                Name = dish.Name,
                                Price = dish.Price,
                                CookingTime = dish.CookingTime,
                                Category = dish.Category,
                                Chef = dish.Chef
                            }).ToList()
                        };

                        cafeNameTextBox.Text = _orderDTO.CafeName;
                        orderDateTimePicker.Value = _orderDTO.OrderDate;

                        // Викликаємо UpdateDishList після завантаження страв
                        UpdateDishList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження замовлень!");
            }
        }

        // Збереження кухарів
        private void SaveChefs()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_chefs);
                File.WriteAllText("chefs.txt", jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час збереження кухарів!");
            }
        }

        // Завантаження кухарів
        private void LoadChefs()
        {
            try
            {
                if (File.Exists("chefs.txt"))
                {
                    string jsonString = File.ReadAllText("chefs.txt");
                    _chefs = JsonSerializer.Deserialize<List<Chef>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження кухарів!");
            }
        }

        // Збереження страв
        private void SaveDishes()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_dishes);
                File.WriteAllText(_dishesFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час збереження страв!");
            }
        }

        // Завантаження страв
        private void LoadDishes()
        {
            try
            {
                if (File.Exists(_dishesFilePath))
                {
                    string jsonString = File.ReadAllText(_dishesFilePath);
                    _dishes = JsonSerializer.Deserialize<List<Dish>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження страв!");
            }
        }

        // ... (інший код OrderForm)
    }
}