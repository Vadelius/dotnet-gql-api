using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using System;
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
        public async Task<IEnumerable<Exercise>> GetExercisesAsync(
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Exercises.ToListAsync(cancellationToken);
    }
}
