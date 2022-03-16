using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Error(Exception ex, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var message = GetMessage(ex);
            return StatusCode(
                statusCode.GetHashCode(), 
                new { 
                    error = message 
                });
        }

        private string GetMessage(Exception ex)
        {
            var errs = new List<string>();

            errs.Add(ex.Message);

            if (ex.InnerException != null)
                errs.Add($"Inner Exception: {ex.InnerException.Message}");

            return string.Join("; ", errs);
        }
    }
}
