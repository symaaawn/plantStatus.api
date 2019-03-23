using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;

namespace plantStatus.api.Services {
    interface ISensorInfoRepository
    {
        IEnumerable<Sensor> GetSensors();
        Sensor GetSensor(string sensorId, bool includeLight);
        IEnumerable<Light> GetLights(string sensorId);
        Light GetLightForSensor(string sensorId, string lightId);
    }
}
