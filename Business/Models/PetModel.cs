#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class PetModel : RecordBase
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Pet Name")]
        public string Name { get; set; }

        public Sex Sex { get; set; }

        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Adopted")]
        public bool IsAdopted { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} must be a positive decimal number in meters!")]
        public decimal? Height { get; set; }

        [Range(0, 100, ErrorMessage = "{0} must be a positive decimal number in kilograms!")]
        public decimal? Weight { get; set; }

        [DisplayName("Species")]
        [Required]
        public int? SpeciesId { get; set; }
        #endregion

        #region Extra Properties
        [DisplayName("Birth Date")]
        public string BirthDateOutput { get; set; }

        [DisplayName("Adopted")]
        public string IsAdoptedOutput { get; set; }

        [DisplayName("Height")]
        public string HeightOutput { get; set; }

        [DisplayName("Weight")]
        public string WeightOutput { get; set; }

        [DisplayName("Species")]
        public string SpeciesOutput { get; set; }
        #endregion
    }
}
