using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiPlanerKlinike.Models
{
    [Table("events")]
    public class ClinicEvent
    {
        public ClinicEvent()
        {

        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        public string Service { get; set; }

        [Required]
        public int Reacurring { get; set; }

        [Required]
        [StringLength(30)]
        public string Patient { get; set; }

        [Required]
        [StringLength(30)]
        public string Doctor { get; set; }

        [Required]
        [StringLength(30)]
        public string Time { get; set; }

        [Required]
        [StringLength(30)]
        public string Date { get; set; }

        [Required]
        [StringLength(30)]
        public string Week { get; set; }

        [Required]
        [StringLength(30)]
        public string CultureInfo { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }
    }
}
