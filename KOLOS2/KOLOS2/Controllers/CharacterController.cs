using KOLOS2.DTOs;
using KOLOS2.Exceptions;
using KOLOS2.Models;
using KOLOS2.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOLOS2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController: ControllerBase
{
    private readonly IDbService _dbService;

    public CharacterController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCharacter(int id)
    {
        try
        {
            var result = await _dbService.GetCharacterDetails(id);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    
    [HttpPost("{id}/backpacks")]
    public async Task<IActionResult> AddItemsToCharacter([FromBody]List<int> items,int id)
    {
        if (! await _dbService.DoesBackpackExist(id))
        {
            return NotFound("Backpack doesn't exist");
        }

        foreach (var item in items)
        {
            if (! await _dbService.DoesItemExist(item))
            {
                return NotFound("Item doesn't exist");
            }
        }
        try
        {
            await _dbService.AddItemsToBackpack(items, id);
            return StatusCode(201);
        }
        catch (ConflictException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    
}