using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class SensorModelForCreation {
        [Required(ErrorMessage = "You should provide a Description")]
        [MaxLength(127)]
        public string Description { get; set; }
    }
}
