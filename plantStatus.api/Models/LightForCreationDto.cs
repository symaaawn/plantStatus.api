using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace plantStatus.api.Models {
    public class LightForCreationDto {
        [Required(ErrorMessage = "You should provide a Value")]
        public int Value { get; set; }
    }
}
