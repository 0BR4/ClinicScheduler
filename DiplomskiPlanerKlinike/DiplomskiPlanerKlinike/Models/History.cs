using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiPlanerKlinike.Models
{
    [Table("history")]
    public class History
    {
        public History()
        {

        }

        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        [Required]
        [StringLength(30)]
        public string Service { get; set; }

        [Required]
        [StringLength(30)]
        public string Time { get; set; }

        [Required]
        [StringLength(30)]
        public string Date { get; set; }
    }
}
