using SoapApi.Dtos;
using SoapApi.Infrastructure.Entities;
using SoapApi.Models;

namespace SoapApi.Mappers;
public static class UserMapper
{
    public static UserModel ToModel(this UserEntity user){

        if(user is null){
            return null;
        }

        return new UserModel

        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.Birthday
        };
    }

    public static UserResponseDto ToDto(this UserModel user)
    {
        return new UserResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate
        };
    }
    

}


    //public static IEnumerable <UserResponseDto> ToDto (this IEnumerable)
    public static UserEntity ToEntity(this UserModel user){
        return new UserEntity{
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Birthday = user.BirthDate

        };
    }

    public static UserModel ToModel(this UserCreateRequestDto user){
        return new UserModel {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = DateTime.UtcNow
        };

    }

    public static UserModel ToModel(this UserUpdateRequestDto user){
        return new UserModel {
            Id = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = DateTime.UtcNow
        };

    }


    

}

