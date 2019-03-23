using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;

namespace plantStatus.api.Controllers {
    public class DummyController : Controller
    {
        private SensorInfoContext _ctx;

        public DummyController (SensorInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/test")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
