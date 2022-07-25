using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Chef : Worker
    {
        private int timer = 0;
        public int Timer { get { return timer; } }

        private string foodName;

        public Chef()
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
                case Menu.Food.noodle:
                    {
                        focus = "라면";
                        myState = focus + " 조리 중";
                        timer = 10;
                        break;
                    }
                case Menu.Food.rabbok:
                    {
                        focus = "라볶이";
                        myState = focus + " 조리 중";
                        timer = 11;
                        break;
                    }
                case Menu.Food.fork:
                    {
                        focus = "제육볶음";
                        myState = focus + " 조리 중";
                        timer = 11;
                        break;
                    }
                case Menu.Food.denjang:
                    {
                        focus = "된장찌개";
                        myState = focus + " 조리 중";
                        timer = 12;
                        break;
                    }
                case Menu.Food.kimchi:
                    {
                        focus = "김치찌개";
                        myState = focus + " 조리 중";
                        timer = 12;
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
