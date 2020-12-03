using System.Threading;
using System.Threading.Tasks;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;

namespace Server.GraphQL.Activities
{
    [ExtendObjectType(Name = "Subscription")]
    public class ActivitySubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Activity> OnActivityAddedAsync(
            [EventMessage] int activityId,
            ActivityByIdDataLoader activityById,
            CancellationToken cancellationToken) =>
            activityById.LoadAsync(activityId, cancellationToken);
    }
}