using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class Worker : People
    {
        public virtual void DoMyWork<T>(T menu) { }
        public virtual string GetMyState() // 현재 상태 뱉기
        {
            return myState;
        }
    }
}
