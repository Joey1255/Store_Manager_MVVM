using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("OrderDiensten")]

public class OrderDienst
{
    [Key]
    public int OrderDienstId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int DienstId { get; set; }

    //navigatieproperties
    public Order Order { get; set; }
    public Dienst Dienst { get; set; }
}
