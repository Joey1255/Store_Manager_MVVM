using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;

[Table("Locaties")]

public partial class Locatie
{
    [Key]
    public int LocatieId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Naam { get; set; }
 
    //Navigatieproperties
    public ICollection<Stock> Stock { get; set; }
}
