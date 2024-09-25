namespace RestApi.Dtos;

public class UserResponse{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; }= null!;
}