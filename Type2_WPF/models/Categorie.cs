using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;

[Table("Categorieën")]
[Index(nameof(Naam), IsUnique = true)]

public partial class Categorie
{
    [Key]
    public int CategorieId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Naam { get; set; }

    [MaxLength(250)]
    public string Beschrijving { get; set; }

    //Navigatieproperties
    public ICollection<Product> Producten { get; set; }
}
