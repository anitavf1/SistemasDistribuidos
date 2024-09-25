using System.CodeDom;
using RestApi.Infrastructure.Soap;
using RestApi.Models;

namespace RestApi.Mappers;

public static class UserMapper {
   public static UserModel ToDomain (this UserResponseDto user){
    if (user is null){
        return null;
    }
    return new UserModel{
        Id = user.UserId,
        FirstName = user.FirstName,
        LastName = user.LastName,
        BirthDay = user.BirthDate,
        Email = user.Email
      
    };
   }
}