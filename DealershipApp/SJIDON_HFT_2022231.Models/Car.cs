using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Model { get; set; }

        public int Horsepower { get; set; }

        public int Price { get; set; }

        [ForeignKey(nameof(Brand))]
        public int Brand_Id { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
    }
}
