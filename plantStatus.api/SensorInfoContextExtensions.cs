using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;
using plantStatus.api.Models;

namespace plantStatus.api {
    public static class SensorInfoContextExtensions 
    {
        public static void EnsureSeedDataForContext(this SensorInfoContext context) 
        {
           if (context.Sensors.Any())
           {
               return;
           }

            var sensors = new List<Sensor>()
            {
                new Sensor()
                {
                    Description = "Sensor 0.",
                    Light = new List<Light>()
                    {
                        new Light()
                        {
                            Value = 10,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new Light()
                        {
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 40,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                },
                new Sensor()
                {
                    Description = "Sensor 1.",
                    Light = new List<Light>()
                    {
                        new Light()
                        {
                            Value = 15,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new Light()
                        {
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 0,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new Light()
                        {
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                }
            };

            context.Sensors.AddRange(sensors);
            context.SaveChanges();
        }
    }
}
