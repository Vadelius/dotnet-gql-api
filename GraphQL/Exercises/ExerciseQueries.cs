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
    public class ExerciseQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Exercise>> GetExercisesAsync(
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Exercises.ToListAsync(cancellationToken);

        public Task<Exercise> GetExerciseByIdAsync(
            [ID(nameof(Exercise))] int id,
            ExerciseByIdDataLoader exerciseById,
            CancellationToken cancellationToken) =>
            exerciseById.LoadAsync(id, cancellationToken);
    }
}
