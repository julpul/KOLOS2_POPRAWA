using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOLOS2.Models;


[Table("Character")]
public class Character
{
    [Key] public int CharacterId { get; set; }
    [MaxLength(50)]public string FirstName { get; set; }
    [MaxLength(120)]public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public ICollection<Backpack> CharacterBackpacks { get; set; }
    public ICollection<CharacterTitle> characterTitles { get; set; }
}