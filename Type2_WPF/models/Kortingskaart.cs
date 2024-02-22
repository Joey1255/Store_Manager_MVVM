using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Kortingskaarten")]
public partial class Kortingskaart
{
    [Key]
    public int KortingskaartId { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public int Percentage { get; set; }
    [Required]
    public bool Professioneel { get; set; }

    public int Teller { get; set; }


    //navigatieproperties
    public Factuur Factuur { get; set; }
}
