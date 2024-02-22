using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("Orders")]
[Index(nameof(Ordernummer), IsUnique = true)]

public partial class Order
{
    [Key]
    public int OrderId { get; set; }
    [Required]
    public string Ordernummer { get; set; }
    [Required]
    public int KlantId { get; set; }

    //Navigatieproperties
    public Klant Klant { get; set; }
    public ICollection<OrderProduct> OrderProducten { get; set; }
    public Factuur Factuur { get; set; }
}
