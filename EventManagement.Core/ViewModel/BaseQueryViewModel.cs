using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.ViewModel
{
    public class BaseQueryViewModel
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string Keyword { get; set; }
    }
}
