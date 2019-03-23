using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using plantStatus.api.Models;
using plantStatus.api.Services;
using AutoMapper;

namespace plantStatus.api.Controllers
{
    [Route("api/sensors")]
    public class SensorsController : Controller
    {
        private ISensorInfoRepository _sensorInfoRepository;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public SensorsController(ISensorInfoRepository repository) {
            _sensorInfoRepository = repository;
        }

        [HttpGet]
        public IActionResult GetSensors()
        {
            var sensorEntities = _sensorInfoRepository.GetSensors();
            var result = Mapper.Map<IEnumerable<SensorWithoutLightDto>>(sensorEntities);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetSensor([FromRoute]Guid id, [FromQuery]bool includeLight = false)
        {
            try
            {
                var sensor = _sensorInfoRepository.GetSensor(id, includeLight);

                if (sensor == null)
                {
                    _log.Warn($"SensorModel {id} does not exist");
                    return NotFound();
                }

                if (includeLight)
                {
                    var sensorResult = Mapper.Map<SensorDto>(sensor);

                    return Ok(sensorResult);
                }

                var sensorWithoutLightResult = Mapper.Map<SensorWithoutLightDto>(sensor);
                return Ok(sensorWithoutLightResult);
            }
            catch (Exception e)
            {
                _log.Error(e, $"Exception while getting sensor {id}");
                return StatusCode(500, "our server did an oopsie");
            }
        }

        [HttpPost(Name = "Post")]
        public IActionResult CreateSensor([FromBody] SensorForCreationDto sensorModelFromRequest)
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

                var finalSensor = Mapper.Map<Entities.Sensor>(sensorModelFromRequest);

                _sensorInfoRepository.AddSensor(finalSensor);

                if (!_sensorInfoRepository.Save()) {
                    return StatusCode(500, "our server did an oopsie");
                }

                var sensorModelForStore = Mapper.Map<Models.SensorDto>(finalSensor);

                return CreatedAtAction("GetSensor", new {id = sensorModelForStore.Id}, sensorModelForStore);
            } 
            catch (Exception e) {
                _log.Error(e, $"Exception while posting new sensor");
                return StatusCode(500, "our server did an oopsie");
            }
        }
    }
}