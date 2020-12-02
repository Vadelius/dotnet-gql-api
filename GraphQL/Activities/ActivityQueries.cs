using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    [ExtendObjectType(Name = "Query")]
    public class ActivityQueries
    {
        [UseApplicationDbContext]
        public async Task<IEnumerable<Activity>> GetActivitiesAsync(
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Activities.ToListAsync(cancellationToken);
    }
}
