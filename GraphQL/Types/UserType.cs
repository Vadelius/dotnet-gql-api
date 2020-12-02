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
                .ResolveWith<ActivityResolvers>(t => t.GetActivitiesAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("activities");

            descriptor
                .Field(t => t.Exercises)
                .ResolveWith<ExerciseResolvers>(t => t.GetExercisesAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("exercises");
        }

        private class ActivityResolvers
        {
            public async Task<IEnumerable<Activity>> GetActivitiesAsync(
                User user,
                [ScopedService] ApplicationDbContext dbContext,
                ActivityByIdDataLoader userById,
                CancellationToken cancellationToken)
            {
                int[] activityIds = await dbContext.Users
                    .Where(s => s.Id == user.Id)
                    .Include(user => user.Activities)
                    .SelectMany(user => user.Activities.Select(activity => activity.CreatedById))
                    .ToArrayAsync();

                return await userById.LoadAsync(activityIds, cancellationToken);
            }
        }
        
        private class ExerciseResolvers
        {
            public async Task<IEnumerable<Exercise>> GetExercisesAsync(
                User user,
                [ScopedService] ApplicationDbContext dbContext,
                ExerciseByIdDataLoader userById,
                CancellationToken cancellationToken)
            {
                int[] exerciseIds = await dbContext.Users
                    .Where(s => s.Id == user.Id)
                    .Include(user => user.Exercises)
                    .SelectMany(user => user.Exercises.Select(exercise => exercise.CreatedById))
                    .ToArrayAsync();

                return await userById.LoadAsync(exerciseIds, cancellationToken);
            }
        }
    }
}