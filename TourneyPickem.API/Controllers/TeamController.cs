using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourneyPickem.Data;
using TourneyPickem.Data.Models;

namespace TourneyPickem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IEnumerable<Team> Get()
    {
        return context.Teams.ToList();
    }

    [HttpGet("{id}", Name = "GetTeamById")]
    public IActionResult GetById(Guid id)
    {
        var Team = context.Teams.Find(id);
        if (Team == null)
        {
            return NotFound();
        }
        return Ok(Team);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Team Team)
    {
        context.Teams.Add(Team);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = Team.Id }, Team);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] Team Team)
    {
        if (id != Team.Id)
        {
            return BadRequest();
        }

        context.Entry(Team).State = EntityState.Modified;
        context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var Team = context.Teams.Find(id);
        if (Team == null)
        {
            return NotFound();
        }

        context.Teams.Remove(Team);
        context.SaveChanges();

        return NoContent();
    }
}
