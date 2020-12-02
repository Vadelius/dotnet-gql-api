using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Exercises
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<User>> GetUsersAsync(
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Users.ToListAsync(cancellationToken);

        public Task<User> GetUserByIdAsync(
            [ID(nameof(User))] int id,
            UserByIdDataLoader userById,
            CancellationToken cancellationToken) =>
            userById.LoadAsync(id, cancellationToken);
    }
}
