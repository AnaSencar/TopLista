using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TopLista
{
    public class Zapis
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Required]
        public int VrijemeUSekundama { get; set; }
        [Required]
        public DateTime DatumUnosa { get; set; }
        public bool Odobreno { get; set; }
    }
}
