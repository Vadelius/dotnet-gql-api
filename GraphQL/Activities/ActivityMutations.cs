using HotChocolate;
using HotChocolate.Types;
using Server.GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    [ExtendObjectType(Name = "Mutation")]
    public class ActivityMutations
    {
        [UseApplicationDbContext]
        public async Task<AddActivityPayload> AddActivityAsync(
            AddActivityInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var activity = new Activity
            {
                Description = input.Description,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                ExerciseId = input.ExerciseId,
                CreatedById = input.CreatedById,
                Points = (int)input.EndTime.Subtract(input.StartTime).TotalMinutes * 1 //TODO: Get Exercies.Duration with ExerciseId?
        };

            context.Activities.Add(activity);
            await context.SaveChangesAsync();

            return new AddActivityPayload(activity);
        }
    }
}
