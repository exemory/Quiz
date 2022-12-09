namespace Data.Entities;

public class Test : EntityBase
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public IEnumerable<Question> Questions { get; set; } = new List<Question>();

    public IEnumerable<User> AllowedUsers { get; set; } = new List<User>();
}