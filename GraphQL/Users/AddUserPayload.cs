using Server.GraphQL.Data;
using Server.GraphQL.Common;
using System.Collections.Generic;

namespace Server.GraphQL.Users
{
    public class AddUserPayload : UserPayloadBase
    {
        public AddUserPayload(User user)
            : base(user)
        {
        }

        public AddUserPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}