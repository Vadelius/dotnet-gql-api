using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.GraphQL;
using Server.GraphQL.Data;
using Server.GraphQL.Users;
using Server.GraphQL.Activities;
using Server.GraphQL.Exercises;
using Server.GraphQL.Types;
using HotChocolate;
using HotChocolate.Execution;
using Snapshooter.Xunit;
using Xunit;

namespace GraphQL.Tests
{
    public class UserTests
    {
        [Fact]
        public async Task User_Schema_Changed()
        {

            ISchema schema = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=server.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddType<UserQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddType<UserMutations>()
                .AddType<UserType>()
                .AddType<ExerciseType>()
                .AddType<ActivityType>()
                .AddFiltering()
                .AddSorting()
                .EnableRelaySupport()
                .BuildSchemaAsync();

            schema.Print().MatchSnapshot();
        }

        [Fact]
        public async Task AddUser()
        {
            IRequestExecutor executor = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=server.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddType<UserQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddType<UserMutations>()
                .AddType<UserType>()
                .AddType<ExerciseType>()
                .AddType<ActivityType>()
                .AddFiltering()
                .AddSorting()
                .EnableRelaySupport()
                .BuildRequestExecutorAsync();

            IExecutionResult result = await executor.ExecuteAsync(@"
             mutation RegisterUser {
                 addUser(
                     input: {
                        name: ""Kurt Kurtan Kurtsson""
                        username: ""Kurt""
                        password: ""123""
                        experience: 1
                                 })
                         {
                             user {
                                 id
                             }
                         }
                     }");

            result.ToJson().MatchSnapshot();
        }
    }
}