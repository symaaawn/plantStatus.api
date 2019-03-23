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
        public Light GetLightForSensor(string sensorId, string lightId)
        {
            return _context.Lights.Where(l => l.SensorId == sensorId && l.Id == lightId).FirstOrDefault();
        }

        public IEnumerable<Light> GetLights(string sensorId)
        {
            return _context.Lights.Where(l => l.SensorId == sensorId).ToList();
        }

        public Sensor GetSensor(string sensorId, bool includeLight) {
            if (includeLight)
            {
                return _context.Sensors.Include(s => s.Light).Where(s => s.Id == sensorId).FirstOrDefault();
            }

            return _context.Sensors.Where(s => s.Id == sensorId).FirstOrDefault();
        }

        public IEnumerable<Sensor> GetSensors() {
            return _context.Sensors.OrderBy(s => s.Id).ToList();
        }
    }
}
