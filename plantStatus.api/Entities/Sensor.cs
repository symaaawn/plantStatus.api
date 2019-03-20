using plantStatus.api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Entities {
    public class Sensor {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(127)]
        public string Description { get; set; }
        public ICollection<Light> Light { get; set; } = new List<Light>();
    }
}
