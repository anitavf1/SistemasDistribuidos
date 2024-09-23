using RestApi.Models;


namespace RestApi.Repositories;

public interface IGroupRepository{

    Task <GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);

}

