using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
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
        [UsePaging]
        public IQueryable<Activity> GetActivities(
            [ScopedService] ApplicationDbContext context) =>
            context.Activities.OrderByDescending(a => a.Created);

        public Task<Activity> GetActivityByIdAsync(
            [ID(nameof(Activity))] int id,
            ActivityByIdDataLoader activityById,
            CancellationToken cancellationToken) =>
            activityById.LoadAsync(id, cancellationToken);
    }
}
