using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class People
    {
        protected string name; // 이름
        public string Name { get { return name; } }
        protected string focus; // 현재 집중하고 있는 것
        public string Focus { get { return focus; } }
        protected string myState; // 현재 상태
        public string State { get { return myState; } }

        public virtual void InitMyName(string myName)
        {
            name = myName;
        }
        public void SetMyFocus<T>(T menu) // 현재 집중하는거
        {
            focus = menu.ToString();
        }
    }
}
