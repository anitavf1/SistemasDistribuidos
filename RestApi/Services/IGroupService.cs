
using RestApi.Models;


namespace RestApi.Services;

public interface IGroupService {
    Task<GroupUserModel>GetGroupByIdAsync(string id, CancellationToken cancellationToken);
    Task<IList<GroupUserModel>>GetGroupByNameAsync(string name,int page, int pageS, string orderBy, CancellationToken cancellationToken); 
}