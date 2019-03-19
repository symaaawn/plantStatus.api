using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using plantStatus.api.Models;

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
        public IActionResult Get([FromRoute]string id)
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

        [HttpPost(Name = "Post")]
        public IActionResult Post([FromBody] SensorModelForCreation sensorModelFromRequest)
        {
            try
            {
                if (sensorModelFromRequest == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                SensorModel sensorModelForStore = new SensorModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = sensorModelFromRequest.Description
                };

                SensorModelDataStore.Current.SensorModels.Add(sensorModelForStore);

                _log.Info($"Created new Sensor {sensorModelForStore.Id}, {sensorModelForStore.Description}");

                return CreatedAtAction("Get", new {id = sensorModelForStore.Id}, sensorModelForStore);
            } 
            catch (Exception e) {
                _log.Error(e, $"Exception while posting new sensor");
                return StatusCode(500, "our server did an oopsie");
            }
        }
    }
}