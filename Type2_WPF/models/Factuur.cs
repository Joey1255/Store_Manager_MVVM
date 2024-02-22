using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Facturen")]

public partial class Factuur
{
    [Key]
    public int FactuurId { get; set; }
    [Required]
    public int BtwPercentage { get; set; }
    [Required]
    public bool BtwNummer { get; set; }
    [Required]
    public int OrderId { get; set; }
    public int? KortingskaartId { get; set; }

    //Navigatieproperties
    public Order Order { get; set; }

    public Kortingskaart Kortingskaart { get; set; }

}
