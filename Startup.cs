
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitleInfoGQL.Data;
using TitleInfoGQL.GraphQL;
using TitleInfoGQL.Models;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TitleInfoGQL.GraphQL.Titles;

namespace TitleInfoGQL
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });
            services.AddPooledDbContextFactory<TitlesContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("TitleInfoGQLConnString")));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()                
                .AddType<Title>()
                .AddType<StoryLine>()
                .AddType<Award>()
                .AddType<Genre>()
                .AddType<OtherName>()
                .AddType<Participant>()                
                .AddType<TitleGenre>()
                .AddType<TitleParticipant>()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = _env.IsDevelopment())
                .AddType<TitleType>()
                .AddFiltering()
                .AddSorting();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            
            app.UseRouting();

            app.UseCors("CorsPolicy");            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

        }
    }
}
