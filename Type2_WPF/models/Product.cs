using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace models;

[Table("Producten")]

[Index(nameof(Productnummer), IsUnique = true)]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Productnummer { get; set; }
    [Required]
    [MaxLength(100)]
    public string Naam { get; set; }
    [Required]
    [Column(TypeName ="money")]
    public decimal Prijs { get; set; }
    [MaxLength(250)]
    public string Beschrijving { get; set; }
    [Required]
    public int CategorieId { get; set; }

    //Navigatieproperties
    public Categorie Categorie { get; set; }
    public ICollection<OrderProduct> OrderProducten { get; set; }
    public ICollection<Stock> Stock { get; set; }

}
