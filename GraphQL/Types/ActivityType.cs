using HotChocolate.Resolvers;
using HotChocolate.Types;
using Server.GraphQL.Data;
using Server.GraphQL.DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Types
{
    public class ActivityType : ObjectType<Activity>
    {
        protected override void Configure(IObjectTypeDescriptor<Activity> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ActivityByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));
        }
    }
}
