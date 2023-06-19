using BalkaBook.Identity.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BalkaBook.Identity.Persistence.Data;

public interface IIdentityDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
