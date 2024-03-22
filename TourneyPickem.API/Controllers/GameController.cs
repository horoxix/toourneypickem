using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyPickem.Data;
using TourneyPickem.Data.Models;

namespace TourneyPickem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Game> Get()
    {
        return context.Games.ToList();
    }

    [HttpGet("{id}", Name = "GetGameById")]
    public IActionResult GetById(Guid id)
    {
        var Game = context.Games.Find(id);
        if (Game == null)
        {
            return NotFound();
        }
        return Ok(Game);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Game game)
    {
        var draftNumbers = Enumerable.Range(1, 8).ToList();
        var random = new Random();

        foreach (var player in game.Players)
        {
            var existingPlayer = context.Players.FirstOrDefault(p => p.Name == player.Name);
            if (existingPlayer == null)
            {
                context.Players.Add(player);
            }

            var index = random.Next(0, draftNumbers.Count);
            player.DraftNumber = draftNumbers[index];
            draftNumbers.RemoveAt(index);
        }

        context.Games.Add(game);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }



    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Game Game)
    {
        if (id != Game.Id)
        {
            return BadRequest();
        }

        context.Entry(Game).State = EntityState.Modified;
        context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var Game = context.Games.Find(id);
        if (Game == null)
        {
            return NotFound();
        }

        context.Games.Remove(Game);
        context.SaveChanges();

        return NoContent();
    }
}
