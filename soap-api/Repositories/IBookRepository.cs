using SoapApi.Dtos;
using SoapApi.Models;

namespace SoapApi.Repositories;

public interface IBookRepository{
    public Task <BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

}