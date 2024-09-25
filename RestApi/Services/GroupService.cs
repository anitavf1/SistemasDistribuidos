using Microsoft.Extensions.Configuration.UserSecrets;
using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService
{
  
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public GroupService (IGroupRepository groupRepository, IUserRepository userRepository) {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }
    
    public async Task<GroupUserModel> GetGroupByIdAsync(string id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);

        if (group is null) {
           return null;
        }

        return new GroupUserModel {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepository.GetByIdAsync(
                    userId, cancellationToken)))).Where(user => user != null)
                    .ToList()
        };
    }

   public async Task<IList<GroupUserModel>> GetGroupByNameAsync(string name,int page, int pageS, string orderBy, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByNameAsync(name, page, pageS, orderBy,  cancellationToken);
    
        if (groups == null || !groups.Any())
        {
            return new List<GroupUserModel>(); 
        }

        return await Task.WhenAll(groups.Select(async group => new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken))))
                .Where(user => user != null)
                .ToList()
        }));
    }

}