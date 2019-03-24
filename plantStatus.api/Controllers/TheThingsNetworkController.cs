using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using plantStatus.api.Entities;
using plantStatus.api.Models;
using plantStatus.api.Services;

namespace plantStatus.api.Controllers
{
    [Route("api/ttn")]
    [ApiController]
    public class TheThingsNetworkController : ControllerBase
    {
        private ISensorInfoRepository _sensorInfoRepository;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public TheThingsNetworkController(ISensorInfoRepository repository) {
            _sensorInfoRepository = repository;
        }

        [HttpPost]
        public IActionResult CreateSensorData([FromBody] TheThingsNetworkDownlinkBodyDto ttnBody)
        {
            Sensor sensor = _sensorInfoRepository.GetSensor(ttnBody.dev_id, false);

            if (sensor == null)
            {
                sensor = new Sensor {
                    Description = ttnBody.dev_id
                };
                _sensorInfoRepository.AddSensor(sensor);

                if (!_sensorInfoRepository.Save()) 
                {
                    return StatusCode(500, "our server did an oopsie");
                }
            }

            dynamic payload_fields = JsonConvert.DeserializeObject(ttnBody.payload_fields.ToString());

            var lightValue = payload_fields.light;
            Light lightFromTtn = new Light()
            {
                Value = lightValue,
                TimeOfMeasurement = DateTime.Now
            };

            sensor = _sensorInfoRepository.GetSensor(ttnBody.dev_id, false);

            _sensorInfoRepository.AddLightForSensor(sensor.Id, lightFromTtn);

            if (!_sensorInfoRepository.Save()) 
            {
                return StatusCode(500, "our server did an oopsie");
            }

            return Ok();
        }
    }
}