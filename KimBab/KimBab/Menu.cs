using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Menu
    {
        public string foodName;
        public int foodPrice;
        public Food food;
        public enum Food
        {
            none,
            normal,
            vegetable,
            tuna,
            beef,
            noodle,
            rabbok,
            fork,
            denjang,
            kimchi,
        }
        public string GetFoodName() { return foodName; }
        public int GetFoodPrice() { return foodPrice; }
        public Food SetFood(int foodNum) // 음식 설정
        {
            switch(foodNum)
            {
                case (int)Food.normal:
                    {
                        foodName = "일반김밥";
                        foodPrice = 1000;
                        food = Food.normal;
                        break;
                    }
                case (int)Food.tuna:
                    {
                        foodName = "참치김밥";
                        foodPrice = 2000;
                        food = Food.tuna;
                        break;
                    }
                case (int)Food.beef:
                    {
                        foodName = "소고기김밥";
                        foodPrice = 2500;
                        food = Food.beef;
                        break;
                    }
                case (int)Food.vegetable:
                    {
                        foodName = "야채김밥";
                        foodPrice = 1500;
                        food = Food.vegetable;
                        break;
                    }
                case (int)Food.rabbok:
                    {
                        foodName = "라볶이";
                        foodPrice = 3000;
                        food = Food.rabbok;
                        break;
                    }
                case (int)Food.kimchi:
                    {
                        foodName = "김치찌개";
                        foodPrice = 3500;
                        food = Food.kimchi;
                        break;
                    }
                case (int)Food.denjang:
                    {
                        foodName = "된장찌개";
                        foodPrice = 3500;
                        food = Food.denjang;
                        break;
                    }
                case (int)Food.fork:
                    {
                        foodName = "제육볶음";
                        foodPrice = 4000;
                        food = Food.fork;
                        break;
                    }
                case (int)Food.noodle:
                    {
                        foodName = "라면";
                        foodPrice = 2000;
                        food = Food.noodle;
                        break;
                    }
                case (int)Food.none:
                    {
                        foodName = "";
                        foodPrice = 0;
                        food = Food.none;
                        break;
                    }
            }
            return food;
        }
    }
}
