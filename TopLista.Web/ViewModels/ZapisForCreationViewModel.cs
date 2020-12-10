using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopLista.Web.ViewModels
{
    public class ZapisForCreationViewModel : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        public int Sat { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Sat == 0 && Min == 0 && Sec == 0)
            {
                yield return new ValidationResult("Vrijeme mora biti uneseno");
            }
            if(Sat < 0 || Min < 0 || Sec < 0)
            {
                yield return new ValidationResult("Sat, minuta i sekunda ne mogu biti negativni");
            }
        }
    }
}
