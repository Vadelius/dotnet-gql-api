using System.Collections.Generic;
using Server.GraphQL.Common;
using Server.GraphQL.Data;

namespace Server.GraphQL.Exercises
{
    public class ExercisePayloadBase : Payload
    {
        protected ExercisePayloadBase(Exercise exercise)
        {
            Exercise = exercise;
        }
        protected ExercisePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
        public Exercise? Exercise { get; }
    }
}
