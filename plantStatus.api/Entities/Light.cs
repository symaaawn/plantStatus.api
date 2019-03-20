using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace plantStatus.api.Entities {
    public class Light {
        [Key]
        public string Id { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public int Value { get; set; }
        public bool LightOn { get; set; }

        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }
        public string SensorId { get; set; }
    }
}
