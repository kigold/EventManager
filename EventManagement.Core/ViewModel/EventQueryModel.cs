using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.ViewModel
{
    public class EventQueryModel : BaseQueryModel
    {
        public int Month { get; set; }
        public string UserId { get; set; }
    }
}
