namespace Answers.Models.Personality;

public class Personality
{
    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public int? Age { get; set; }

    public Gender? Gender { get; set; }

    public Guid? UserId { get; set; }
}