using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Diensten")]

public partial class Dienst
{
    [Key]
    public int DienstId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Naam { get; set; }
    [Required]
    [Column(TypeName = "money")]
    public decimal Prijs { get; set; }
    [Required]
    [MaxLength(250)]
    public string Beschrijving { get; set; }
}
