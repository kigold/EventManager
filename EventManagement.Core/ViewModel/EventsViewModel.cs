using EventManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.ViewModel
{
    public class EventsViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsRecuring { get; set; }

        public static implicit operator EventsViewModel(Event model)
        {
            return model == null ? null : new EventsViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DateCreated = model.DateCreated,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                IsRecuring = model.IsRecuring,
                UserId = model.UserId.ToString()
            };
        }
    }
}
