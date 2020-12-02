using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Types
{
    public class ExerciseType : ObjectType<Exercise>
    {
        protected override void Configure(IObjectTypeDescriptor<Exercise> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ExerciseByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.CreatedBy)
                .ResolveWith<UserResolvers>(t => t.GetUserAsync(default!, default!, default));

            descriptor
                .Field(t => t.CreatedById)
                .ID(nameof(User));

            descriptor
                .Field(t => t.Activities)
                .ResolveWith<ActivityResolvers>(t => t.GetActivitiesAsync(default!, default!, default!, default))
                .Name("activities");
        }

        private class ActivityResolvers
        {
            [UseApplicationDbContext]
            public async Task<IEnumerable<Activity>> GetActivitiesAsync(
                Exercise exercise,
                [ScopedService] ApplicationDbContext dbContext,
                ActivityByIdDataLoader activityById,
                CancellationToken cancellationToken)
            {
                int[] Ids = await dbContext.Exercises
                    .Where(s => s.Id == exercise.Id)
                    .Include(s => s.Activities)
                    .SelectMany(s => s.Activities.Select(activity => activity.ExerciseId))
                    .ToArrayAsync();

                return await activityById.LoadAsync(Ids, cancellationToken);
            }
        }

        private class UserResolvers
        {
            [UseApplicationDbContext]
            public async Task<User?> GetUserAsync(
                Exercise exercise,
                UserByIdDataLoader userById,
                CancellationToken cancellationToken)
            {
                return await userById.LoadAsync(exercise.CreatedById, cancellationToken);
            }
        }
    }
}

