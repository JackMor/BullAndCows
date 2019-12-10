using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Model
{
    class History
    {
        List<HistoryItem> history = new List<HistoryItem>();
        
        public List<HistoryItem> GetHistoryItems
        {
            get { return history; }
        }

        public void AddHistoryItem(HistoryItem hi)
        {
            history.Add(hi);
        }

    }
}
