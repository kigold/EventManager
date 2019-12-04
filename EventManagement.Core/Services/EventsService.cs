using EventManagement.Core.Context;
using EventManagement.Core.Model;
using EventManagement.Core.Services.Interface;
using EventManagement.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EventManagement.Core.Services
{
    public class EventsService : IEventsService
    {

        private readonly EventMgtDbContext _context;
        private List<ValidationResult> _validations = new List<ValidationResult>();
        public EventsService(EventMgtDbContext context)
        {
            _context = context;
        }

        public ResponseViewModel AddEvent(EventsViewModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
                _validations.Add(new ValidationResult("Title is required"));
            if (model.StartDate == null)
                _validations.Add(new ValidationResult("Start Date is required"));
            if (_validations.Any())
                return new ResponseViewModel(_validations);
            Guid? userId;
            if(!Guid.TryParse(model.UserId, out var userGuid))
            {
                //userId = userGuid == Guid.Empty ? null : userGuid;
                if(userGuid == Guid.Empty)
                {
                    userId = null;
                }
                else
                {
                    userId = userGuid;
                }
            }
            var eventData = new Event
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate == null ? model.StartDate : model.EndDate,
                DateCreated = DateTime.Now,
                IsRecuring = model.IsRecuring,
                UserId = userGuid
            };
            _context.Add(eventData);
            _context.SaveChanges();
            model.Id = eventData.Id;

            return new ResponseViewModel(_validations);
        }

        public ResponseViewModel DeleteEvent(int id)
        {
            var eventData = _context.Events.FirstOrDefault(x => x.Id == id);
            if (eventData == null)
            {
                _validations.Add(new ValidationResult("Event Data not found"));
                return new ResponseViewModel(_validations);
            }
            _context.Remove(eventData);
            _context.SaveChanges();
            return new ResponseViewModel(_validations);
        }

        public ResponseViewModel EditEvent(EventsViewModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
                _validations.Add(new ValidationResult("Title is required"));
            if (model.StartDate == null)
                _validations.Add(new ValidationResult("Start Date is required"));
            if (_validations.Any())
                return new ResponseViewModel(_validations);
            Guid? userId;
            if (!Guid.TryParse(model.UserId, out var userGuid))
            {
                //userId = userGuid == Guid.Empty ? null : userGuid;
                if (userGuid == Guid.Empty)
                {
                    userId = null;
                }
                else
                {
                    userId = userGuid;
                }
            }
            var eventData = _context.Events.FirstOrDefault(x => x.Id == model.Id);
            if (eventData == null)
            { 
                _validations.Add(new ValidationResult("Event Data not found"));
                return new ResponseViewModel(_validations);
            }
            eventData.Title = model.Title;
            eventData.Description = model.Description;
            eventData.StartDate = model.StartDate;
            eventData.EndDate = model.EndDate == null ? model.StartDate : model.EndDate;
            eventData.DateCreated = DateTime.Now;
            eventData.IsRecuring = model.IsRecuring;
            eventData.UserId = userGuid;
            
            _context.Update(eventData);
            _context.SaveChanges();

            return new ResponseViewModel(_validations);
        }

        public ResponseViewModel<IEnumerable<EventsViewModel>> GetAllEvents(BaseQueryViewModel modeld)
        {
            var query = _context.Events.AsQueryable();
            var result = query.ToList();
            return new ResponseViewModel<IEnumerable<EventsViewModel>>(_validations, 
                result.Select(x => (EventsViewModel)x));
        }

        public ResponseViewModel<EventsViewModel> GetEvent(int id)
        {
            var result = _context.Events.FirstOrDefault(x => x.Id == id);
            if (result == null)
                _validations.Add(new ValidationResult("Event not found"));
            return new ResponseViewModel<EventsViewModel>(_validations,
                result);
        }

        public ResponseViewModel<IEnumerable<EventsViewModel>> GetEvents(BaseQueryViewModel query)
        {
            int page = (query.PageIndex == null || query.PageIndex < 1) ? 1 : query.PageIndex.Value;
            int pageSize = (query.PageSize == null || query.PageSize < 1) ? 10 : query.PageSize.Value;

            var result = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(query.Keyword))
                result = result.Where(x => x.Title.ToLower().Contains(query.Keyword) 
                    || x.Description.ToLower().Contains(query.Keyword));

            var totalCount = result.Count();

            result = result.Skip((page - 1) * pageSize).Take(pageSize);

            return new ResponseViewModel<IEnumerable<EventsViewModel>>(_validations,
                result.ToList().Select(x => (EventsViewModel)x), totalCount);
        }
    }
}
