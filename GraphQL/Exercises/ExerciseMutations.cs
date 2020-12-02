using HotChocolate;
using HotChocolate.Types;
using Server.GraphQL.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Exercises
{
    [ExtendObjectType(Name = "Mutation")]
    public class ExerciseMutations
    {
        [UseApplicationDbContext]
        public async Task<AddExercisePayload> AddExerciseAsync(
            AddExerciseInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var exercise = new Exercise
            {
                Name = input.Name,
                Description = input.Description,
                Multiplier = input.Multiplier,
                CreatedById = input.CreatedById,
            };

            context.Exercises.Add(exercise);
            await context.SaveChangesAsync();

            return new AddExercisePayload(exercise);
        }
    }
}
