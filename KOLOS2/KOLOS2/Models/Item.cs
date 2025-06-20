using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOLOS2.Models;

[Table("Item")]
public class Item
{
    [Key] public int ItemId { get; set; }
    [MaxLength(100)] public string Name { get; set; }
    public int Weight { get; set; }


    public ICollection<Backpack> BackpackItems { get; set; }
}