using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyPickem.Data;
using TourneyPickem.Data.Models;

namespace TourneyPickem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Player> Get()
    {
        return context.Players.ToList();
    }

    [HttpGet("{id}", Name = "GetPlayerById")]
    public IActionResult GetById(Guid id)
    {
        var player = context.Players.Find(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpGet("ByGameId/{gameId}")]
    public IEnumerable<Player> GetByGameId(Guid gameId)
    {
        return context.Players.Where(p => p.Game.Id == gameId).ToList();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Player player)
    {
        context.Players.Add(player);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = player.Id }, player);
    }

    [HttpPost("AddMultiple")]
    public IActionResult AddMultiple([FromBody] IEnumerable<Player> players)
    {
        if (players == null || !players.Any())
        {
            return BadRequest("No players provided.");
        }

        // Validate that there are exactly 8 players
        if (players.Count() != 8)
        {
            return BadRequest("Exactly 8 players must be provided.");
        }

        context.Players.AddRange(players);
        context.SaveChanges();

        return Ok("Players added successfully.");
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Player player)
    {
        if (id != player.Id)
        {
            return BadRequest();
        }

        context.Entry(player).State = EntityState.Modified;
        context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var player = context.Players.Find(id);
        if (player == null)
        {
            return NotFound();
        }

        context.Players.Remove(player);
        context.SaveChanges();

        return NoContent();
    }
}
