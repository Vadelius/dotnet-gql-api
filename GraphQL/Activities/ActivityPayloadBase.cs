using Server.GraphQL.Common;
using Server.GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Activities
{
    public class ActivityPayLoadBase : Payload
    {
        protected ActivityPayLoadBase(Activity activity)
        {
            Activity = activity;
        }
        protected ActivityPayLoadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
        public Activity? Activity { get; }
    }
}
