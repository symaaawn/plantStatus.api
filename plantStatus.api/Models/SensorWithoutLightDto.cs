using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class SensorWithoutLightDto {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
