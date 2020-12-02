using Server.GraphQL.Common;
using Server.GraphQL.Data;

namespace Server.GraphQL.Exercises
{
    public class AddExercisePayload : Payload
    {
        public AddExercisePayload(Exercise exercise)
        {
            Exercise = exercise;
        }

        public AddExercisePayload(UserError error)
            : base(new[] { error })
        {
        }

        public Exercise? Exercise { get; init; }
    }
}
