using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? UserId { get; set; }
        public bool IsRecuring { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
