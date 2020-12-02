using System.Collections.Generic;

namespace Server.GraphQL.Exercises
{
    public record AddExerciseInput(
        string Name,
        string Description,
        double Multiplier,
        int CreatedById);
}
