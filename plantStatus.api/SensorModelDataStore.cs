﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Models;

namespace plantStatus.api {
    public class SensorModelDataStore {
        public static SensorModelDataStore Current { get; } = new SensorModelDataStore();
        public List<SensorModel> SensorModels { get; set; }

        public SensorModelDataStore()
        {
            SensorModels = new List<SensorModel>()
            {
                new SensorModel()
                {
                    Id = "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4",
                    Description = "Sensor 0.",
                    Light = new List<LightModel>()
                    {
                        new LightModel()
                        {
                            Id = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 10,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new LightModel()
                        {
                            Id = "1234501F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = "96534A0A-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = "2ABCD01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 40,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = "336DA01F-9ABD-4d9d-80C7-02AF85C822A8",
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 20, 0),
                            LightOn = false
                        },
                    }
                },
                new SensorModel()
                {
                    Id = "ABC68C5E-CEB2-4faa-B6BF-329BF39FA1E4",
                    Description = "Sensor 1.",
                    Light = new List<LightModel>()
                    {
                        new LightModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 15,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 0, 0),
                            LightOn = false
                        },
                        new LightModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 30,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 5, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 20,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 10, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Value = 0,
                            TimeOfMeasurement = new DateTime(2019, 3, 19, 12, 15, 0),
                            LightOn = false
                        },new LightModel()
                        {
                            Id = Guid.NewGuid().ToString(),
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
