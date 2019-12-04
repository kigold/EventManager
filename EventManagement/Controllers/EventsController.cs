using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Core.Services.Interface;
using EventManagement.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        private readonly IEventsService _eventService;
        public EventsController(IEventsService eventsService)
        {
            _eventService = eventsService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EventsViewModel), 200)]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _eventService.GetEvent(id);
                if (result.Validations.Any())
                    return ApiResponse<EventsViewModel>(errors: result.Validations.Select(x => x.ErrorMessage).ToArray());
                return ApiResponse<EventsViewModel>(result.Payload);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<EventsViewModel>), 200)]
        public IActionResult Gets([FromQuery]BaseQueryViewModel query)
        {
            try
            {
                var result = _eventService.GetEvents(query);
                if (result.Validations.Any())
                    return ApiResponse<IEnumerable<EventsViewModel>>(errors: result.Validations.Select(x => x.ErrorMessage).ToArray());
                return ApiResponse<IEnumerable<EventsViewModel>>(result.Payload, totalCount:result.TotalCount);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EventsViewModel), 200)]
        public IActionResult Gets(EventsViewModel model)
        {
            try
            {
                if(model == null)
                    return ApiResponse<EventsViewModel>(errors: "Empty Payload");
                var result = _eventService.AddEvent(model);
                if (result.Validations.Any())
                    return ApiResponse<EventsViewModel>(errors: result.Validations.Select(x => x.ErrorMessage).ToArray());
                return ApiResponse<EventsViewModel>(model);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EventsViewModel), 200)]
        public IActionResult Update(EventsViewModel model)
        {
            try
            {
                if (model == null)
                    return ApiResponse<EventsViewModel>(errors: "Empty Payload");
                var result = _eventService.EditEvent(model);
                if (result.Validations.Any())
                    return ApiResponse<EventsViewModel>(errors: result.Validations.Select(x => x.ErrorMessage).ToArray());
                return ApiResponse<EventsViewModel>(model);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EventsViewModel), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                    return ApiResponse<EventsViewModel>(errors: "Id is required");
                var result = _eventService.DeleteEvent(id);
                if (result.Validations.Any())
                    return ApiResponse<EventsViewModel>(errors: result.Validations.Select(x => x.ErrorMessage).ToArray());
                return ApiResponse<string>("Successfully deleted");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}