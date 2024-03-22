using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TourneyPickem.Data.Models;

public class Player
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Team> Teams { get; set; } = new();
    public int DraftNumber { get; set; }
    public int Wins { get; set; }

    [JsonIgnore]
    public Game? Game { get; set; }
}
