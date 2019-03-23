using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class SensorDto {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ICollection<LightDto> Light { get; set; } = new List<LightDto>();
    }
}
