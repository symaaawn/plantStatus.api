using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace plantStatus.api.Controllers
{
    [Route("api/sensors")]
    public class SensorsController : Controller
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(SensorModelDataStore.Current.SensorModels);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            try
            {
                var sensorModelToReturn = SensorModelDataStore.Current.SensorModels.FirstOrDefault(s => s.Id == id);
                if (sensorModelToReturn == null)
                {
                    _log.Warn($"SensorModel {id} does not exist");
                    return NotFound();
                }

                return Ok(sensorModelToReturn);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting sensor {id}");
                return StatusCode(500, "our server did an oopsie");
            }
        }
    }
}