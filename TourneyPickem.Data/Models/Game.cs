using System.ComponentModel.DataAnnotations;

namespace TourneyPickem.Data.Models;

public class Game
{
    [Key]
    public Guid Id { get; set; }
    public int Year { get; set; }
    public List<Player> Players { get; set; } = new();
}
