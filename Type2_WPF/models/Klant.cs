using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Klanten")]

public partial class Klant
{
    [Key]
    public int Klantid { get; set; }
    [Required]
    [MaxLength(50)]
    public string Telefoonnummer { get; set; }
    [Required]
    [MaxLength(150)]
    public string Straat { get; set; }
    [Required]
    [MaxLength(10)]
    public string Huisnummer { get; set; }
    [Required]
    [MaxLength(40)]
    public string Gemeente { get; set; }
    [Required]
    [MaxLength(40)]
    public string Email { get; set; }

    [MaxLength(100)]
    public string? Voornaam { get; set; }
    [MaxLength(100)]
    public string? Achternaam { get; set; }
    [MaxLength(100)]
    public string? Bedrijfsnaam { get; set; }
    [MaxLength(50)]
    public string? Btwnummer { get; set; }

    [Required]
    public bool Professioneel { get; set; }

    //Navigatieproperties
    public ICollection<Order> Orders { get; set; }

}
