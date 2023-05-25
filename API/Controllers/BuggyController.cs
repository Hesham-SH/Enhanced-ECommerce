using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly SiteContext _siteContext;
        public BuggyController(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFound()
        {
            var thing = _siteContext.Products.Find(50);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var thing = _siteContext.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}