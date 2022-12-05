using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panel.API.Filters;
using Panel.DTOs;

namespace Panel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreatActionResult<T>(CustomResponseDto<T> responseDto)
        {
            if (responseDto.statusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = responseDto.statusCode
                };
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.statusCode
            };
        }
    }
}
