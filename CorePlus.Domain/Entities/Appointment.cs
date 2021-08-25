using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CorePlus.Domain.Entities
{
    public class Appointment : CorePlusBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }

        public double? Duration { get; set; }

        public decimal? Revenue { get; set; }

        public decimal? Cost { get; set; }


        #region Relationships
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }

        [ForeignKey("AppointmentType")]
        public int TypeId { get; set; }

        public AppointmentType Type { get; set; }

        [ForeignKey("Practitioner")]
        public int PractitionerId { get; set; }

        public Practitioner Practitioner { get; set; }
        #endregion
    }
}
