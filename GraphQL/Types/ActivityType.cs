using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Types
{
    public class ActivityType : ObjectType<Activity>
    {
        protected override void Configure(IObjectTypeDescriptor<Activity> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ActivityByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.CreatedBy)
                .ResolveWith<UserResolvers>(t => t.GetUserAsync(default!, default!, default));

            descriptor
                .Field(t => t.CreatedById)
                .ID(nameof(User));

            descriptor
                .Field(t => t.Exercise)
                .ResolveWith<ExerciseResolvers>(t => t.GetExerciseAsync(default!, default!, default));

            descriptor
                .Field(t => t.ExerciseId)
                .ID(nameof(Exercise));
        }
        private class UserResolvers
        {
            [UseApplicationDbContext]
            public async Task<User?> GetUserAsync(
                Activity activity,
                UserByIdDataLoader userById,
                CancellationToken cancellationToken)
            {
                return await userById.LoadAsync(activity.CreatedById, cancellationToken);
            }
        }

        private class ExerciseResolvers
        {
            [UseApplicationDbContext]
            public async Task<Exercise?> GetExerciseAsync(
                Activity activity,
                ExerciseByIdDataLoader exerciseById,
                CancellationToken cancellationToken)
            {
                return await exerciseById.LoadAsync(activity.ExerciseId, cancellationToken);
            }
        }
    }
}
