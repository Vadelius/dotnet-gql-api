using Server.GraphQL.Common;
using Server.GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    public class AddActivityPayload : Payload
    {
        public AddActivityPayload(Activity activity)
        {
            Activity = activity;
        }

        public AddActivityPayload(UserError error)
            : base(new[] { error })
        {
        }

        public Activity? Activity { get; init; }
    }
}
