using plantStatus.api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Entities {
    public class Sensor {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(127)]
        public string Description { get; set; }
        public ICollection<Light> Light { get; set; } = new List<Light>();
    }
}
