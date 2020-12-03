using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Server.GraphQL.Common;
using Server.GraphQL.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    [ExtendObjectType(Name = "Mutation")]
    public class ActivityMutations
    {
        [UseApplicationDbContext]
        public async Task<AddActivityPayload> AddActivityAsync(
            AddActivityInput input,
            [ScopedService] ApplicationDbContext context,
            [Service]ITopicEventSender eventSender)
        {
            if (string.IsNullOrEmpty(input.Description))
            {
                return new AddActivityPayload(
                    new UserError("Description cannot be empty.", "DESCRIPTION_EMPTY"));
            }

            if (input.EndTime < input.StartTime)
            {
                return new AddActivityPayload(
                    new UserError("EndTime has to occur after StartTime.", "END_TIME_INVALID"));
            }

            User user = await context.Users.FindAsync(input.CreatedById);

            if (user is null)
            {
                return new AddActivityPayload(
                    new UserError("No User found with that ID.", "NO_USER_FOUND_FOR_ID"));
            }

            Exercise exercise = await context.Exercises.FindAsync(input.ExerciseId);

            if (exercise is null)
            {
                return new AddActivityPayload(
                    new UserError("No Exercise found with that ID.", "NO_EXERCISE_FOUND_FOR_ID"));
            }

            var activity = new Activity
            {
                Description = input.Description,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                ExerciseId = input.ExerciseId,
                CreatedById = input.CreatedById,
                Points = (int)(input.EndTime.Subtract(input.StartTime).TotalMinutes * exercise.Multiplier)
            };

            context.Activities.Add(activity);

            await context.SaveChangesAsync();

            await eventSender.SendAsync(
                nameof(ActivitySubscriptions.OnActivityAddedAsync),
                activity.Id);

            return new AddActivityPayload(activity);
        }
    }
}
