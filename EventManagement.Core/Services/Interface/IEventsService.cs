using EventManagement.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Core.Services.Interface
{
    public interface IEventsService
    {
        ResponseViewModel<IEnumerable<EventsViewModel>> GetEvents(BaseQueryViewModel query);
        ResponseViewModel<IEnumerable<EventsViewModel>> GetAllEvents(BaseQueryViewModel query);
        ResponseViewModel<EventsViewModel> GetEvent(int id);
        ResponseViewModel AddEvent(EventsViewModel model);
        ResponseViewModel EditEvent(EventsViewModel model);
        ResponseViewModel DeleteEvent(int id);
    }
}
