using KOLOS2.Models;

namespace KOLOS2.DTOs;

public class CharacterDetailsDto
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public int currentWeight { get; set; }
    public int maxWeight { get; set; }

    public List<BackpackItemsDto> backpackItems { get; set; }
    public List<TitlesDto> titles { get; set; }
}

public class BackpackItemsDto
{
    public string itemName { get; set; }
    public int itemWeight { get; set; }
    public int amount { get; set; }
}

public class TitlesDto
{
    public string title { get; set; }
    public DateTime aquiredAt { get; set; }
}