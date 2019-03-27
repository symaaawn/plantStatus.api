using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace plantStatus.api.Services {
    public class SensorInfoRepository : ISensorInfoRepository
    {
        private SensorInfoContext _context;

        public SensorInfoRepository(SensorInfoContext context)
        {
            _context = context;
        }

        public void AddLightForSensor(Guid sensorId, Light light)
        {
            var sensor = GetSensor(sensorId, false);
            sensor.Light.Add(light);
        }

        public void AddSensor(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
        }

        public Light GetLight(Guid sensorId, Guid lightId)
        {
            return _context.Lights.Where(l => l.SensorId == sensorId && l.Id == lightId).FirstOrDefault();
        }

        public IEnumerable<Light> GetLights(Guid sensorId)
        {
            return _context.Lights.Where(l => l.SensorId == sensorId).ToList();
        }

        public Sensor GetSensor(Guid sensorId, bool includeLight) {
            if (includeLight)
            {
                return _context.Sensors.Include(s => s.Light).Where(s => s.Id == sensorId).FirstOrDefault();
            }

            return _context.Sensors.Where(s => s.Id == sensorId).FirstOrDefault();
        }

        public Sensor GetSensor(string sensorDescription, bool includeLight) {
            if (includeLight) {
                return _context.Sensors.Include(s => s.Light).Where(s => s.Description == sensorDescription).FirstOrDefault();
            }

            return _context.Sensors.Where(s => s.Description == sensorDescription).FirstOrDefault();
        }

        public IEnumerable<Sensor> GetSensors() {
            return _context.Sensors.OrderBy(s => s.Id).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool SensorExists(Guid sensorId)
        {
            return _context.Sensors.Any(s => s.Id == sensorId);
        }
    }
}
