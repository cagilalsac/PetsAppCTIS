#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Pet : RecordBase
    {
        // Way 1:
        //public string Name { get; set; } = null!;
        // Way 2:
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Sex Sex { get; set; }

        public DateTime BirthDate { get; set; }
        public bool IsAdopted { get; set; }

        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }

        public int SpeciesId { get; set; } // foreign key
        public Species Species { get; set; }
    }
}
