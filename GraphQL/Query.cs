using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.GraphQL.Data;
using HotChocolate;
using Server.GraphQL.DataLoader;
using System.Threading;
using HotChocolate.Types.Relay;

namespace Server.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
            context.Users.ToListAsync();
        public Task<List<Exercise>> GetExercises([ScopedService] ApplicationDbContext context) =>
            context.Exercises.ToListAsync();
        public Task<List<Activity>> GetActivities([ScopedService] ApplicationDbContext context) =>
            context.Activities.ToListAsync();

        public Task<User> GetUserAsync(
            [ID(nameof(User))] int id,
            UserByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

    }
}