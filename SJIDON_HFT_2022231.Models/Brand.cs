using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SJIDON_HFT_2022231.Models
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Owner { get; set; }

        [ForeignKey(nameof(Dealership))]
        public int Dealership_Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Dealership Dealership { get; set; }

    }
}
