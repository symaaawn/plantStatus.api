using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class LightModel {
        public  string Id { get; set; }
        public int Value { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
