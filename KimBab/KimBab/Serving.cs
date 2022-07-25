using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Serving : Worker // 요리를 요청하는 역할 << 이 친구가 매개체가 됨
    {
        private int chefMenu;
        public int ChefMenu { get { return chefMenu; } set { chefMenu = value; } }

        private int kimMenu;
        public int KimMenu { get { return kimMenu; } set { kimMenu = value; } }
        public Serving()
        {
            DoMyWork(ServeState.none);
        }
        public enum ServeState
        {
            ordering,
            none,
        }
        public override void InitMyName(string myName)
        {
            name = myName;
        }
        public override void DoMyWork<T>(T state)
        {
            switch(state)
            {
                case ServeState.ordering:
                    {
                        if(focus != "")
                        {
                            myState = focus + " 주문 받는 중";
                        }
                        break;
                    }
                case ServeState.none:
                    {
                        myState = "대기 중";
                        break;
                    }
            }
        }
    }
}
