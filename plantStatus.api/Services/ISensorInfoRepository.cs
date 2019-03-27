using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;

namespace plantStatus.api.Services {
    public interface ISensorInfoRepository
    {
        bool SensorExists(Guid sensorId);
        IEnumerable<Sensor> GetSensors();
        Sensor GetSensor(Guid sensorId, bool includeLight);
        Sensor GetSensor(string sensorDescription, bool includeLight);
        IEnumerable<Light> GetLights(Guid sensorId);
        Light GetLight(Guid sensorId, Guid lightId);
        void AddLightForSensor(Guid cityId, Light light);
        void AddSensor(Sensor sensor);
        bool Save();
    }
}
