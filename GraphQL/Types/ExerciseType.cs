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
    public class ExerciseType : ObjectType<Exercise>
    {
        protected override void Configure(IObjectTypeDescriptor<Exercise> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ExerciseByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }

        private class SessionResolvers
        {
            //[UseApplicationDbContext]
            //public async Task<IEnumerable<Exercise>> GetExercisesAsync(
            //    Exercise exercise,
            //    [ScopedService] ApplicationDbContext dbContext,
            //    ExerciseByIdDataLoader speakerById,
            //    CancellationToken cancellationToken)
            //{
            //    int[] speakerIds = await dbContext.Exercises
            //        .Where(s => s.Id == exercise.Id)
            //        .Include(s => s.CreatedById)
            //        .ToArrayAsync();

            //    return await speakerById.LoadAsync(speakerIds, cancellationToken);
            //}
        }
    }
}
