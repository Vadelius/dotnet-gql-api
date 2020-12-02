using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System.Collections.Generic;
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

        public Task<Activity> GetActivityByIdAsync(
            [ID(nameof(Activity))] int id,
            ActivityByIdDataLoader activityById,
            CancellationToken cancellationToken) =>
            activityById.LoadAsync(id, cancellationToken);
    }
}
