using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class LightDto {
        public Guid Id { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public int Value { get; set; }
        public bool LightOn { get; set; }
    }
}
