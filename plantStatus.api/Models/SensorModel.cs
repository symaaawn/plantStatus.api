using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class SensorModel {
        public string Id { get; set; }
        public string Description { get; set; }
        public ICollection<LightModel> Light { get; set; } = new List<LightModel>();
    }
}
