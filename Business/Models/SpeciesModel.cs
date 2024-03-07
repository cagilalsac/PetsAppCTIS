#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class SpeciesModel : RecordBase
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "{0} must be minimum {2} and maximum {1} characters!")]
        [DisplayName("Species Name")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        [DisplayName("Pet Count")]
        public int PetCountOutput { get; set; }

        [DisplayName("Pet Names")]
        public string PetNamesOutput { get; set; }
        #endregion
    }
}
