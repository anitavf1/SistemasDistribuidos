using SoapApi.Dtos;
using SoapApi.Infrastructure.Entities;
using SoapApi.Models;

namespace SoapApi.Mappers;
public static class BookMapper
{
    public static BookModel ToModel(this BookEntity book){

        if(book is null){
            return null;
        }

        return new BookModel

        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate
        };
    }

    public static BookResponseDto ToDto(this BookModel book)
    {
        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Publisher = book.Publisher,
            PublishedDate = book.PublishedDate
        };
    }

    //public static IEnumerable <UserResponseDto> ToDto (this IEnumerable)
    public static BookEntity ToEntity(this BookModel Book){
        return new BookEntity{
            Id = Book.Id,
            Title = Book.Title,
            Author = Book.Author,
            Publisher = Book.Publisher,
            PublishedDate = Book.PublishedDate
        };
    }
    

    


    

}

