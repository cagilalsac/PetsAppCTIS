#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Species : RecordBase
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
