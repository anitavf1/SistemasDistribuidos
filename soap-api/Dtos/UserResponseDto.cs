using System.Runtime.Serialization;

namespace SoapApi.Dtos;

public class UserResponseDto
{
    [DataMember]
    public Guid UserId { get; set; }
    [DataMember]
    public String Email { get; set; } = null!;
    [DataMember]
    public String FirstName { get; set; } = null!;
    [DataMember]
    public String LastName { get; set; } = null!;
    [DataMember]
    public DateTime BirthDate { get; set; }

}