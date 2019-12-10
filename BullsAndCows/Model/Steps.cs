using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Model
{
    class Steps
    {
        List<StepsItem> steps = new List<StepsItem>();

        public List<StepsItem> GetStepsItems
        {
            get { return steps; }
        }

        public void AddStepsItem(StepsItem si)
        {
            steps.Add(si);
        }

        public void Clear()
        {
            steps.Clear();
        }
    }
}
