using System.ComponentModel.DataAnnotations;

namespace TourneyPickem.Data.Models;

public class Team
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OverallSeed { get; set; }
    public int Seed { get; set; }
    public Region Region { get; set; }
}
