using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOLOS2.Models;

[Table("Title")]
public class Title
{
    [Key]public int TitleId { get; set; }
    public string Name { get; set; }

    public ICollection<CharacterTitle> Type { get; set; }
}