using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure.Entities;

namespace SoapApi.Infrastructure;

public class RelationalDbContext : DbContext
{


    public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options)
    {

    }


    public DbSet<UserEntity> Users { get; set; }


    internal async Task UpdateAsync(UserEntity userEntity)
    {
        throw new NotImplementedException();
    }
}



