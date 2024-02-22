using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Medewerkers")]

public partial class Medewerker
{
    //nog niet zeker dat we deze klasse nodig gaan hebben, maar blijft voorlopig nog staan
    [Key]
    public int MedewerkerId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Voornaam { get; set; }
    [Required]
    [MaxLength(100)]
    public string Achternaam { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(40)]
    public string Paswoord { get; set; }
}
