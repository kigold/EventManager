using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EventManagement.Core.Enums;
using EventManagement.Core.Helpers;
using EventManagement.Core.ViewModel;
using EventManagement.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        //private readonly ILogger<BaseController> _logger;
        public BaseController()
        {
            //_logger = ServiceLocator.Current.GetInstance<ILogger<BaseController>>();
        }

        protected UserPrincipal CurrentUser
        {
            get
            {
                return new UserPrincipal(User as ClaimsPrincipal);
            }
        }

        public IActionResult ApiResponse<T>(T data = default(T), string message = "",
            ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors) where T : class
        {
            ApiResponse<T> response = new ApiResponse<T>
            {
                TotalCount = totalCount ?? 0,
                Errors = errors.ToList(),
                Payload = data,
                Code = !errors.Any() ? codes : codes == ApiResponseCodes.OK ? ApiResponseCodes.ERROR : codes
            };

            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }

        protected ApiResponse<IEnumerable<string>> GetModelStateValidationErrorsAsList()
        {
            var response = new ApiResponse<IEnumerable<string>>
            {
                Code = ApiResponseCodes.ERROR
            };
            var message = ModelState.Values.SelectMany(a => a.Errors).Select(e => e.ErrorMessage);
            var list = new List<string>();
            list.AddRange(message);
            response.Payload = list;
            return response;
        }

        protected string GetModelStateValidationErrors()
        {
            string message = string.Join("; ", ModelState.Values
                                    .SelectMany(a => a.Errors)
                                    .Select(e => e.ErrorMessage));
            return message;
        }

        protected string GetModelStateValidationError()
        {
            string message = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
            return message;
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            //_logger.LogError(ex, ex.Message);


            ApiResponse<string> rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCodes.ERROR;
#if DEBUG
            rsp.Errors = new List<string>() { $"Error: {(ex?.InnerException?.Message ?? ex.Message)} --> {ex?.StackTrace}" };
            return Ok(rsp);
#else
             rsp.Errors = new List<string>() {  customErrorMessage ?? "An error occurred while processing your request!"};
             return Ok(rsp);
#endif
        }
    }
}