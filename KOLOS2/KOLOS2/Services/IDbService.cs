using KOLOS2.DTOs;

namespace KOLOS2.Services;

public interface IDbService
{
    public Task<CharacterDetailsDto> GetCharacterDetails(int id);
    public Task<bool> DoesBackpackExist(int id);
    public Task<bool> DoesItemExist(int id);
    public Task AddItemsToBackpack(List<int> items, int id);
}