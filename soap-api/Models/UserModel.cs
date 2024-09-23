using SoapApi.Dtos;

namespace SoapApi.Models;

public class UserModel{

    public Guid Id { get; set; }
    public String Email { get; set; } = null!;
    public String FirstName { get; set; } = null!;
    public String LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }

    internal object Select(Func<object, UserResponseDto> value)
    {
        throw new NotImplementedException();
    }

}

