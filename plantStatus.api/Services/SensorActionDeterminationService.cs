using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plantStatus.api.Entities;

namespace plantStatus.api.Services {
    public class SensorActionDeterminationService {
        public bool TurnLightOn(DateTime now)
        {
            //Todo: turn on light depending on the sun duration during day
            bool lightOn;
            if (now.Hour <= 4 || now.Hour >= 22) {
                lightOn = true;
            } else {
                lightOn = false;
            }
            return lightOn;
        }

        public Light AutofillLight(Light light)
        {
            light.TimeOfMeasurement = DateTime.Now;
            if (light.TimeOfMeasurement.Hour <= 4 || light.TimeOfMeasurement.Hour >= 22) {
                light.LightOn = true;
            } else {
                light.LightOn = false;
            }

            return light;
        }
    }
}
