namespace Business.DataTransferObjects;

public class UserInfoDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}