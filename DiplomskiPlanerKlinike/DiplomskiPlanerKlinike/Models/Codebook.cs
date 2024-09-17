using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiPlanerKlinike.Models
{
    [Table("codebook")]
    public class Codebook
    {
        public Codebook()
        {

        }

        public int Id { get; set; }

        [Required]
        public String Code { get; set; }

        [Required]
        public String Disease { get; set; }

    }
}
