using System.Reflection;
using Server.GraphQL.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Server.GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ApplicationDbContext>();
        }
    }
}