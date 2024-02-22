using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;

[Table("Stock")]
public partial class Stock
{
    [Key]
    public int StockId { get; set; }
    [Required]
    public int Aantal { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int LocatieId { get; set; }
    
    //navigatieproperties
    public Product Product { get; set; }
    public Locatie Locatie { get; set; }
}
