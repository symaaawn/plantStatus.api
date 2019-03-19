using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using plantStatus.api.Models;

namespace plantStatus.api.Controllers
{
    [Route("api/sensors")]
    [ApiController]
    public class LightsController : Controller
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        [HttpGet("{sensorId}/light")]
        public IActionResult Get([FromRoute] string sensorId)
        {
            try
            {
                var sensor = SensorModelDataStore.Current.SensorModels.FirstOrDefault(s => s.Id == sensorId);
                if (sensor == null)
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                return Ok(sensor.Light);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting lights from {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpGet("{sensorId}/light/{id}")]
        public IActionResult Get([FromRoute] string sensorId, [FromRoute] string id)
        {
            try
            {
                var sensor = SensorModelDataStore.Current.SensorModels.FirstOrDefault(s => s.Id == sensorId);
                if (sensor == null)
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                var light = sensor.Light.FirstOrDefault(l => l.Id == id);
                if (light == null)
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                return Ok(light);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting light {id} from {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpPost("{sensorId}/light")]
        public IActionResult Post([FromRoute] string sensorId,
            [FromBody] LightModelForCreation lightModelFromRequest)
        {
            try
            {
                if (lightModelFromRequest == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var sensor = SensorModelDataStore.Current.SensorModels.FirstOrDefault(s => s.Id == sensorId);
                if (sensor == null)
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                LightModel finalLightModel = new LightModel()
                {
                    Value = lightModelFromRequest.Value,
                    Id = Guid.NewGuid().ToString(),
                    TimeOfMeasurement = DateTime.Now
                };

                sensor.Light.Add(finalLightModel);

                //Light will turn on at night.
                //Todo: turn on light depending on the sun duration during day
                bool lightOn;
                if (finalLightModel.TimeOfMeasurement.Hour <= 4 || finalLightModel.TimeOfMeasurement.Hour >= 22)
                {
                    lightOn = true;
                }
                else
                {
                    lightOn = false;
                }

                LightControlModel lightControlModel = new LightControlModel()
                {
                    LightOn = lightOn
                };

                _log.Info($"Added value {lightModelFromRequest.Value} to Sensor {sensorId}. Turn light on: {lightOn}");

                return CreatedAtAction("Get", new
                {
                    sensorId, id = finalLightModel.Id
                }, lightControlModel);

            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while posting light for {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpPut("{sensorId}/light/{id}")]
        public IActionResult Put([FromRoute] string sensorId, [FromRoute] string id,
            [FromBody] LightModelForCreation lightModelFromRequest)
        {
            try
            {
                if (lightModelFromRequest == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var sensor = SensorModelDataStore.Current.SensorModels.FirstOrDefault(s => s.Id == sensorId);
                if (sensor == null)
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                var lightModelFromStore = sensor.Light.FirstOrDefault(l => l.Id == id);
                if (lightModelFromStore == null)
                {
                    _log.Warn($"LightModel {id} does not exist");
                    return NotFound();
                }

                lightModelFromStore.Value = lightModelFromRequest.Value;

                _log.Info($"Updated LightValue {lightModelFromStore.Id} to Value {lightModelFromStore.Value}");

                return NoContent();
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while putting light {id} for {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }
    }
}