using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Models;

namespace plantStatus.api {
    public class SensorModelDataStore {
        public static SensorModelDataStore Current { get; } = new SensorModelDataStore();
        public List<SensorDto> SensorModels { get; set; }

        public SensorModelDataStore()
        {
            SensorModels = new List<SensorDto>()
            {
                new SensorDto()
                {
                    Id = new Guid(),
                    Description = "Sensor 0.",
                    Light = new List<LightDto>()
                    {
                        new LightDto()
                        {
                            Id = new Guid(),
                            Value = 10,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new LightDto()
                        {
                            Id = new Guid(),
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 40,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                },
                new SensorDto()
                {
                    Id = new Guid(),
                    Description = "Sensor 1.",
                    Light = new List<LightDto>()
                    {
                        new LightDto()
                        {
                            Id = new Guid(),
                            Value = 15,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new LightDto()
                        {
                            Id = new Guid(),
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 0,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new LightDto()
                        {
                            Id = new Guid(),
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                }
            };
        }
    }
}
