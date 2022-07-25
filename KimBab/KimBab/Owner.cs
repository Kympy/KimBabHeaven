using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Owner : Worker
    {
        public int timer = 0;
        public bool isWorking = false;
        public Owner()
        {
            focus = "";
            myState = "대기 중";
        }
        public enum OwnerState // 주인 상태
        {
            cleaning,
            none,
        }
        public override void InitMyName(string myName)
        {
            name = myName;
        }
        public override void DoMyWork<T>(T clean) // 주인 일
        {
            switch(clean)
            {
                case OwnerState.cleaning:
                    {
                        if(focus != "")
                        {
                            myState = focus + " 청소 중";
                        }
                        break;
                    }
                case OwnerState.none:
                    {
                        myState = "대기 중";
                        break;
                    }
            }
        }
    }
}
