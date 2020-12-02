using System.Threading.Tasks;
using Server.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;
using Server.GraphQL.Users;

namespace Server.GraphQL
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
        [UseApplicationDbContext]
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var user = new User
            {
                Name = input.Name,
                Username = input.Username,
                Password = input.Password,
                Experience = input.Experience
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AddUserPayload(user);
        }
    }
}