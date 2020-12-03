using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using Server.GraphQL.Types;
using Server.GraphQL.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Exercises
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<UserType>))]
        [UseFiltering(typeof(UserFilterInputType))]
        [UseSorting]
        public IQueryable<User> GetUsers(
            [ScopedService] ApplicationDbContext context) =>
            context.Users.OrderBy(e => e.Id);
        // TODO: Perhaps add by XP as an alternative for betting indexing.


        public Task<User> GetUserByIdAsync(
            [ID(nameof(User))] int id,
            UserByIdDataLoader userById,
            CancellationToken cancellationToken) =>
            userById.LoadAsync(id, cancellationToken);
    }
}
