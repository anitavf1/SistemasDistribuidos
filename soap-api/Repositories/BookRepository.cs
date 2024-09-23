using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;
using SoapApi.Models;
using SoapApi.Mappers;
using SoapApi.Dtos;
using System.ServiceModel;

namespace SoapApi.Repositories;

public class BookRepository : IBookRepository {
    private readonly RelationalDbContext _dbContext;
    public BookRepository(RelationalDbContext dbContext) {
        _dbContext = dbContext;
    }
    public async Task<BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return book.ToModel();
    }

}