using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    public record AddActivityInput(
        string Description,
        int Points,
        DateTime StartTime,
        DateTime EndTime,
        int ExerciseId,
        int CreatedById);

}
