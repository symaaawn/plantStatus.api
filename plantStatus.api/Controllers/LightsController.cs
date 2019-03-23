using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using plantStatus.api.Models;
using plantStatus.api.Services;

namespace plantStatus.api.Controllers
{
    [Route("api/sensors")]
    [ApiController]
    public class LightsController : Controller
    {
        private ISensorInfoRepository _sensorInfoRepository;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public LightsController(ISensorInfoRepository repository) {
            _sensorInfoRepository = repository;
        }

        [HttpGet("{sensorId}/light")]
        public IActionResult GetLights([FromRoute] Guid sensorId)
        {
            try
            {
                if (!_sensorInfoRepository.SensorExists(sensorId))
                {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                var lightsForSensor = _sensorInfoRepository.GetLights(sensorId);

                var lightsForSensorResult = Mapper.Map<IEnumerable<LightDto>>(lightsForSensor);

                return Ok(lightsForSensorResult);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting lights from {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpGet("{sensorId}/light/{id}")]
        public IActionResult GetLight([FromRoute] Guid sensorId, [FromRoute] Guid id)
        {
            try
            {
                if (!_sensorInfoRepository.SensorExists(sensorId)) {
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                var light = _sensorInfoRepository.GetLight(sensorId, id);

                if (light == null)
                {
                    _log.Warn($"LightModel {id} does not exist");
                    return NotFound();
                }

                var lightResult = Mapper.Map<LightDto>(light);

                return Ok(lightResult);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting light {id} from {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpPost("{sensorId}/light")]
        public IActionResult CreateLight([FromRoute] Guid sensorId,
            [FromBody] LightForCreationDto lightModelFromRequest)
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

                if (!_sensorInfoRepository.SensorExists(sensorId)) 
                { 
                    _log.Warn($"SensorModel {sensorId} does not exist");
                    return NotFound();
                }

                var finalLight = Mapper.Map<Entities.Light>(lightModelFromRequest);

                DateTime now = DateTime.Now;

                //Light will turn on at night.
                //Todo: turn on light depending on the sun duration during day
                bool lightOn;
                if (now.Hour <= 4 || now.Hour >= 22) {
                    lightOn = true;
                } else {
                    lightOn = false;
                }

                finalLight.TimeOfMeasurement = now;
                finalLight.LightOn = lightOn;

                _sensorInfoRepository.AddLightForSensor(sensorId, finalLight);

                if (!_sensorInfoRepository.Save())
                {
                    return StatusCode(500, "our server did an oopsie");
                }

                var lightModelForStore = Mapper.Map<Models.LightDto>(finalLight);
                
                return CreatedAtAction("GetLight", new
                {
                    sensorId, id = lightModelForStore.Id
                }, lightModelForStore);

            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while posting light for {sensorId}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpPut("{sensorId}/light/{id}")]
        public IActionResult Put([FromRoute] Guid sensorId, [FromRoute] Guid id,
            [FromBody] LightForCreationDto lightModelFromRequest)
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