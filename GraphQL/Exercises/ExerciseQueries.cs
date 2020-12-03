using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Exercises
{
    [ExtendObjectType(Name = "Query")]
    public class ExerciseQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Exercise> GetExercises(
            [ScopedService] ApplicationDbContext context) =>
            context.Exercises.OrderBy(e => e.Multiplier);

        public Task<Exercise> GetExerciseByIdAsync(
            [ID(nameof(Exercise))] int id,
            ExerciseByIdDataLoader exerciseById,
            CancellationToken cancellationToken) =>
            exerciseById.LoadAsync(id, cancellationToken);
    }
}
