

using KOLOS2.Data;
using KOLOS2.DTOs;
using KOLOS2.Exceptions;
using KOLOS2.Models;
using Microsoft.EntityFrameworkCore;

namespace KOLOS2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CharacterDetailsDto> GetCharacterDetails(int id)
    {
        var result = await _context.Characters.Where(c => c.CharacterId == id)
            .Select(c => new CharacterDetailsDto
            {
                firstName = c.FirstName,
                lastName = c.LastName,
                currentWeight = c.CurrentWeight,
                maxWeight = c.MaxWeight,
                backpackItems = c.CharacterBackpacks.Select(b => new BackpackItemsDto
                {
                    amount = b.Amount,
                    itemName = b.Item.Name,
                    itemWeight = b.Item.Weight,
                }).ToList(),
                titles = c.characterTitles.Select(t => new TitlesDto()
                {
                    title = t.Title.Name,
                    aquiredAt = t.AcquiredAt
                }).ToList(),
            }).FirstOrDefaultAsync();

        if (result == null)
        {
            throw new NotFoundException();
        }
        return result;
    }

    public async Task<bool> DoesBackpackExist(int id)
    {
        return await _context.Backpacks.AnyAsync(c => c.CharacterId == id);
    }

    public async Task<bool> DoesItemExist(int id)
    {
        return  await _context.Items.AnyAsync(i => i.ItemId == id);
    }

    public async Task AddItemsToBackpack(List<int> items, int id)
    {

        var waightsum = _context.Items.Where(i => items.Contains(i.ItemId)).Select(i => i.Weight).Sum();
        var currentweight = _context.Characters.Where(c => c.CharacterId == id).Select(c => c.CurrentWeight).First();
        var maxweight = _context.Characters.Where(c => c.CharacterId == id).Select(c => c.CurrentWeight).First();
        if (currentweight+waightsum > maxweight)
        {
            throw new ConflictException();
            
        }
        var newWaightsum = currentweight + waightsum;
        
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            
            var newItems = _context.Items.Where(i => items.Contains(i.ItemId)).Select(i => new Item()
            {
                ItemId = i.ItemId,
                Name = i.Name,
                Weight = i.Weight,
            });
            
            var result = newItems.Select(i => new Backpack()
            {
                CharacterId = id,
                ItemId = i.ItemId,
                Amount = newItems.Count()
            }).ToList();
            await _context.Backpacks.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            
            var character = await _context.Characters.Where(c => c.CharacterId == id).FirstOrDefaultAsync();
            character.CurrentWeight = newWaightsum;
            
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}