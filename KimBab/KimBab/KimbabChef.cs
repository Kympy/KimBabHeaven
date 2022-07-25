using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class KimbabChef : Worker
    {
        private int timer = 0;
        public int Timer { get { return timer; } }
        public KimbabChef()
        {
            myState = "대기 중";
        }
        public override void InitMyName(string myName)
        {
            name = myName;
        }
        public override void DoMyWork<T>(T menu)
        {
            switch(menu)
            {
                case Menu.Food.normal:
                    {
                        focus = "일반김밥";
                        myState = focus + " 마는 중";
                        timer = 3;
                        break;
                    }
                case Menu.Food.vegetable:
                    {
                        focus = "야채김밥";
                        myState = focus + " 마는 중";
                        timer = 3;
                        break;
                    }
                case Menu.Food.tuna:
                    {
                        focus = "참치김밥";
                        myState = focus + " 마는 중";
                        timer = 4;
                        break;
                    }
                case Menu.Food.beef:
                    {
                        focus = "소고기김밥";
                        myState = focus + " 마는 중";
                        timer = 4;
                        break;
                    }
                case Menu.Food.none:
                    {
                        myState = "대기 중";
                        timer = 0;
                        break;
                    }
            }
        }
    }
}
