using HotChocolate.Data.Filters;
using Server.GraphQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.GraphQL.Users
{
    public class UserFilterInputType : FilterInputType<User>
    {
        protected override void Configure(IFilterInputTypeDescriptor<User> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.Activities);
            descriptor.Ignore(t => t.Exercises);
        }
    }
}