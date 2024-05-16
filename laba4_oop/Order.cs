using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4_oop
{
    [Serializable]
    public class Order
    {
        private string _cafeName;
        private DateTime _orderDate;
        public string DishesData { get; set; }
        public Order(string cafeName, DateTime orderDate)
        {
            _cafeName = cafeName;
            _orderDate = orderDate;
            Dishes = new List<Dish>(); // Инициализируем список блюд
        }

        public string CafeName
        {
            get { return _cafeName; }
            set { _cafeName = value; }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        public List<Dish> Dishes { get; set; } // Добавьте список для хранения блюд

        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
        }

        public string ToShortString()
        {
            int totalCookingTime = Dishes.Sum(dish => dish.CookingTime);
            return $"Кафе: {_cafeName}, Дата: {_orderDate.ToShortDateString()}, Час очікування: {totalCookingTime} хв";
        }

        public override string ToString()
        {
            return $"Замовлення: {_cafeName}\nДата: {_orderDate.ToShortDateString()}\nСтрави:\n" +
                   string.Join("\n", Dishes.Select(dish => $"  - {dish.Name}, ціна: {dish.Price}, час приготування: {dish.CookingTime} хв, категорія: {dish.Category}, повар: {dish.Chef.FirstName} {dish.Chef.LastName}"));
        }
    }
}
