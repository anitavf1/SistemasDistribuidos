
using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;
using SoapApi.Models;
using SoapApi.Mappers;

using SoapApi.Dtos;
using System.ServiceModel;

namespace SoapApi.Repositories;

public class UserRepository : IUserRepository {
    private readonly RelationalDbContext _dbContext;
    public UserRepository(RelationalDbContext dbContext) {

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


    public async Task DeleteByIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        var userEntity = user.ToEntity();
        _dbContext.Users.Remove(userEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }

    public async Task<UserModel> CreateAsync(UserModel user, CancellationToken cancellationToken)
    {
        var userEntity = user.ToEntity();
        userEntity.Id = Guid.NewGuid();
        await _dbContext.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userEntity.ToModel();
    }


    public async Task<UserModel> UpdateAsync(UserModel user, CancellationToken cancellationToken)
    {
        var userExistent= await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id==user.Id, cancellationToken);
        if(userExistent is null){
            throw new FaultException("User not found");
        }

        userExistent.FirstName = user.FirstName;
        userExistent.LastName = user.LastName;
        userExistent.Birthday= user.BirthDate;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        return userExistent.ToModel();
    }

}