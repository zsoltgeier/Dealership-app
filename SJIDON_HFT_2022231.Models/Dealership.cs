using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Models
{
    [Table("Dealerships")]
    public class Dealership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        
        public int Employees { get; set; }  //number of employees working at the dealership

        [NotMapped]
        public virtual ICollection<Brand> Brands { get; set; }

        public Dealership()
        {
            Brands = new HashSet<Brand>();
        }
    }
}
