using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class FluentCondition
    {
        public FluentCondition Match(bool condition, Action action)
        {
            if (condition)
            {
                action();
            }

            return this;
        }
    }
}
