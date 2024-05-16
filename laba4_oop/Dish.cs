using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4_oop
{
    public class Dish
    {
        private string _name;
        private int _price;
        private int _cookingTime;
        private Category _category;
        private Chef _chef;


        public Dish(string name, int price, int cookingTime, Category category, Chef chef)
        {
            _name = name;
            _price = price;
            _cookingTime = cookingTime;
            _category = category;
            _chef = chef;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int CookingTime
        {
            get { return _cookingTime; }
            set { _cookingTime = value; }
        }

        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public Chef Chef
        {
            get { return _chef; }
            set { _chef = value; }
        }
    }
}
