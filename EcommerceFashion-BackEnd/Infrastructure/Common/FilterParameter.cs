using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class FilterParameter
    {
        private int _pageSize;

        public FilterParameter()
        {
            _pageSize = MinPageSize;
        }

        #region Pagination

        protected virtual int MinPageSize { get; set; } = Domain.Common.Constant.DefaultMinPageSize;
        protected virtual int MaxPageSize { get; set; } = Domain.Common.Constant.DefaultMaxPageSize;
        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < MinPageSize ? MinPageSize : value;
        }

        #endregion

        #region Filter

        public string? Search { get; set; }
        public string Order { get; set; } = string.Empty;
        public bool OrderByDescending { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        #endregion
    }
}
