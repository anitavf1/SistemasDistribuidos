using System.ServiceModel;
using SoapApi.Dtos;

namespace SoapApi.Contracts;

[ServiceContract]

public interface IBookContract{
    [OperationContract]
    public Task <BookResponseDto> GetBookById(Guid BookId, CancellationToken cancellationToken);
    
}

