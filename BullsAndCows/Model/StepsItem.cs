using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Model
{
    class StepsItem
    {
        long step;
        long code;
        string result;

        public StepsItem(long step, long code, string result)
        {
            this.step = step;
            this.code = code;
            this.result = result;
        }

        public long Step
        {
            get { return step; }
            set { step = value; }
        }
        public long Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

    }
}
