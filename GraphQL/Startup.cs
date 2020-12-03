using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.GraphQL;
using Server.GraphQL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.GraphQL.DataLoader;
using Server.GraphQL.Types;
using Server.GraphQL.Exercises;
using Server.GraphQL.Activities;

namespace Server.GraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(options => options.UseSqlite("Data Source=server.db"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<UserQueries>()
                    .AddTypeExtension<ActivityQueries>()
                    .AddTypeExtension<ExerciseQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<UserMutations>()
                    .AddTypeExtension<ExerciseMutations>()
                    .AddTypeExtension<ActivityMutations>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<ActivitySubscriptions>()
                .AddType<UserType>()
                .AddType<ExerciseType>()
                .AddType<ActivityType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<UserByIdDataLoader>()
                .AddDataLoader<ActivityByIdDataLoader>()
                .AddDataLoader<ExerciseByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
