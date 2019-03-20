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
                    Id = "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4",
                    Description = "Sensor 0.",
                    Light = new List<Light>()
                    {
                        new Light()
                        {
                            Id = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 10,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new Light()
                        {
                            Id = "1234501F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = "96534A0A-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = "2ABCD01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 40,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = "336DA01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                },
                new Sensor()
                {
                    Id = "ABC68C5E-CEB2-4faa-B6BF-329BF39FA1E4",
                    Description = "Sensor 1.",
                    Light = new List<Light>()
                    {
                        new Light()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 15,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new Light()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 0,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new Light()
                        {
                            Id = Guid.NewGuid().ToString(),
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
