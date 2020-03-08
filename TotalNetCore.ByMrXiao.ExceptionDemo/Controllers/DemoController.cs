using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TotalNetCore.ByMrXiao.ExceptionDemo.Exceptions;

namespace TotalNetCore.ByMrXiao.ExceptionDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [Route("g")]
        [HttpGet]
        public IActionResult Index()
        {
            //throw new Exception("error");
            throw new MyServerException("error", 65);
            return Ok();
        }
    }
}