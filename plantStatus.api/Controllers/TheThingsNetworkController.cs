using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private TheThingsNetworkDownlinkService _uplinkService;
        private SensorActionDeterminationService _sensorActionDetermination;

        public TheThingsNetworkController(ISensorInfoRepository repository, TheThingsNetworkDownlinkService uplinkService, SensorActionDeterminationService sensorActionDetermination) {
            _sensorInfoRepository = repository;
            _uplinkService = uplinkService;
            _sensorActionDetermination = sensorActionDetermination;
        }

        [HttpPost]
        public IActionResult CreateSensorData([FromBody] TheThingsNetworkUplinkBodyDto ttnBody)
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

            if (lightValue != null)
            {
                LightForCreationDto lightFromTtn = new LightForCreationDto()
                {
                    Value = lightValue
                };

                var finalLight = Mapper.Map<Entities.Light>(lightFromTtn);

                finalLight = _sensorActionDetermination.AutofillLight(finalLight);

                sensor = _sensorInfoRepository.GetSensor(ttnBody.dev_id, false);

                _sensorInfoRepository.AddLightForSensor(sensor.Id, finalLight);

                if (!_sensorInfoRepository.Save()) {
                    return StatusCode(500, "our server did an oopsie");
                }
            }

            TheThingsNetworkDownlinkBodyDto downlinkBody = new TheThingsNetworkDownlinkBodyDto()
            {
                dev_id = ttnBody.dev_id,
                confirmed = false,
                payload_raw = "01",
                port = 1
            };

            _uplinkService.QueueDownlink(ttnBody.downlink_url, downlinkBody);

            return Ok();
        }
    }
}