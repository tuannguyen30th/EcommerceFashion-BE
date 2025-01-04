using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class PaginationResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public T Data { get; set; } = null!;
    }
}
