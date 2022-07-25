using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Table
    {
        public string tableNumber; // 테이블 번호
        public string person; // 담당 사람

        public string state; // 테이블 상태
        public TableState now;

        public string dish; // 요리
        public Menu.Food nowMenu;

        public int timer; // 식사 시간
        public int price = 0;
        public bool isUsing = false;

        public Table()
        {
            now = TableState.empty;
            SwitchTableState(TableState.empty, "none", 0);
        }
        
        public enum TableState // 테이블 상태
        {
            empty,
            cleaning,
            waitOrder,
            waitDish,
            eating,
            needClean,
        }
        public void SwitchTableState(TableState nowState, string personName, int menu)
        {
            nowMenu = GameManager.Instance.GetMenu.SetFood(menu); // 음식 설정
            dish = GameManager.Instance.GetMenu.GetFoodName(); // 이름 가져오기
            price = GameManager.Instance.GetMenu.GetFoodPrice(); // 가격 가져오기
            person = personName;
            switch (nowState) // 테이블 현재 상태
            {                     
                case TableState.empty:
                    {
                        state = "비어 있음";
                        now = TableState.empty;
                        price = 0;
                        isUsing = false;
                        break;
                    }
                case TableState.cleaning:
                    {
                        state = person + ", 청소 중";
                        now = TableState.cleaning;
                        break;
                    }
                case TableState.waitOrder:
                    {
                        state = person + ", 주문 대기 중";
                        now = TableState.waitOrder;
                        isUsing = true;
                        break;
                    }
                case TableState.waitDish:
                    {
                        state = person + ", 요리 대기 중, " + dish;
                        now = TableState.waitDish;
                        break;
                    }
                case TableState.needClean:
                    {
                        state = "청소 필요함";
                        now = TableState.needClean;
                        break;
                    }
                case TableState.eating:
                    {
                        state = person + ", 식사 중, " + dish;
                        now = TableState.eating;
                        break;
                    }
            }
        }
    }
}
