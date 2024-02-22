using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models;
[Table("OrderProducten")]

public class OrderProduct
{
    [Key]
    public int OrderProductId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int OrderId { get; set; }
    
    //navigatieproperties
    public Product Product { get; set; }
    public Order Order { get; set; }
   
}
