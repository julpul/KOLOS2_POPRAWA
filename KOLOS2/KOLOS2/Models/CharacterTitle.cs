using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KOLOS2.Models;

[Table("Character_Title")]
[PrimaryKey(nameof(TitleId),nameof(CharacterId))]
public class CharacterTitle
{
    [ForeignKey("Title")]public int TitleId { get; set; }
    [ForeignKey("Character")]public int CharacterId { get; set; }
    public DateTime AcquiredAt { get; set; }

    public Title Title { get; set; }
    public Character Character { get; set; }
}