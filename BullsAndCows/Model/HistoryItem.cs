using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Model
{
    class HistoryItem
    {
        long gameNum;
        long code;
        long steps;
        bool result;

        public HistoryItem(long gameNum, long code, long steps, bool result)
        {
            this.gameNum = gameNum;
            this.code = code;
            this.steps = steps;
            this.result = result;
        }

        public long GameNum
        {
            get { return gameNum; }
            set { gameNum = value;}
        }
        public long Code
        {
            get { return code; }
            set { code = value; }
        }
        public long Steps
        {
            get { return steps; }
            set { steps = value; }
        }
        public string GetResult
        {
            get
            {
                if(result == true)
                {
                    return "+";
                }
                else
                {
                    return "-";
                }
            }
        }
        public bool SetResult
        {
            set{ result = value; }
        }
        



    }
}
