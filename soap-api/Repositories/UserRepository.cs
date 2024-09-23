
using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;
using SoapApi.Models;
using SoapApi.Mappers;


namespace SoapApi.Repositories;

public class UserRespository : IUserRepository {
    private readonly RelationalDbContext _dbContext;
    public UserRespository(RelationalDbContext dbContext) {

        _dbContext = dbContext;
    }
    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return user.ToModel();
    }

    public async Task<IList<UserModel>> GetAllAsync(CancellationToken cancellationToken) {
        var users = await _dbContext.Users.AsNoTracking().Select(user => user.ToModel()).ToListAsync(cancellationToken);
        return users;
    }

    public async Task<IList<UserModel>> GetAllByEmailAsync(string email, CancellationToken cancellationToken)
    {
         var users = await _dbContext.Users.AsNoTracking().Where(user => user.Email.Contains(email)).Select(user => user.ToModel()).ToListAsync(cancellationToken);
         return users;
    }

}