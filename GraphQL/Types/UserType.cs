using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;

namespace Server.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<UserByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Activities)
                .ResolveWith<ActivityResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("activities");
        }

        private class ActivityResolvers
        {
            public async Task<IEnumerable<Activity>> GetSessionsAsync(
                User user,
                [ScopedService] ApplicationDbContext dbContext,
                ActivityByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] userIds = await dbContext.Users
                    .Where(s => s.Id == user.Id)
                    .Include(s => s.Activities)
                    .SelectMany(s => s.Activities.Select(t => t.CreatedById))
                    .ToArrayAsync();

                return await sessionById.LoadAsync(userIds, cancellationToken);
            }
        }
    }
}