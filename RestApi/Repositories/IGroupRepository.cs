using RestApi.Models;

namespace RestApi.Repositories;
public interface IGroupRepository
{
    Task<GroupModel> GetByIdAsync (string id, CancellationToken cancellationToken);
    Task<IList<GroupModel>> GetByNameAsync (string name, int page, int pageS, string orderBy, CancellationToken cancellationToken);


    Task<IList<GroupModel>> GetByExactNameAsync (string name, int page, int pageS, string orderBy, CancellationToken cancellationToken);

    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);

    Task <GroupModel> CreateAsync(string name, Guid [] Users, CancellationToken cancellationToken);


    Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken);
}

    

