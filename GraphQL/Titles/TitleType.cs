using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using TitleInfoGQL.Data;
using TitleInfoGQL.Models;

namespace TitleInfoGQL.GraphQL.Titles
{
    public class TitleType:ObjectType<Title>
    {
        protected override void Configure(IObjectTypeDescriptor<Title> descriptor)
        {
            descriptor.Description("Represents any movie title");
            descriptor.Field(t => t.TitleId)
                .Description("Represents the unique id for title");

            descriptor.Field(t => t.TitleName).Description("Title of the movie");

            descriptor.Field(t => t.TitleNameSortable).Description("the title name is sortable");

            //More info on default! = null-forgiving operator here: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving

            descriptor.Field(t => t.StoryLines).ResolveWith<Resolvers>(t => t.GetStoryLines(default!, default!))
            .UseDbContext<TitlesContext>()
            .Description("This is the list of story lines for the title in question");

            descriptor.Field(t => t.Awards).ResolveWith<Resolvers>(t => t.GetAwards(default!, default!))
                .UseDbContext<TitlesContext>()
                .Description("These are the list of awards for the title in question");

            descriptor.Field(t => t.OtherNames).ResolveWith<Resolvers>(t => t.GetOtherNames(default!, default!))
                .UseDbContext<TitlesContext>()
                .Description("These are the list of Other Names for the title in question");
            
            descriptor.Field(t => t.TitleGenres).ResolveWith<Resolvers>(t => t.GetTitleGenres(default!, default!))
               .UseDbContext<TitlesContext>()
               .Description("These are the list of Title Genres for the title in question");
            
            descriptor.Field(t => t.TitleParticipants).ResolveWith<Resolvers>(t => t.GetTitleParticipants(default!, default!))
               .UseDbContext<TitlesContext>()
               .Description("These are the list of Title Participants for the title in question");
        }



        private class Resolvers
        {
            public IQueryable<StoryLine> GetStoryLines(Title title, [ScopedService] TitlesContext context)
            {
                return context.StoryLines.Where(t => t.TitleId == title.TitleId);
            }

            public IQueryable<Award> GetAwards(Title title, [ScopedService] TitlesContext context)
            {
                return context.Awards.Where(t => t.TitleId == title.TitleId);
            }

            public IQueryable<OtherName> GetOtherNames(Title title, [ScopedService] TitlesContext context)
            {
                return context.OtherNames.Where(t => t.TitleId == title.TitleId);
            }

            public IQueryable<TitleGenre> GetTitleGenres(Title title, [ScopedService] TitlesContext context)
            {
                return context.TitleGenres.Where(t => t.TitleId == title.TitleId);
            }

            
            public IQueryable<TitleParticipant> GetTitleParticipants(Title title, [ScopedService] TitlesContext context)
            {
                return context.TitleParticipants.Where(t => t.TitleId == title.TitleId);
            }
        }
    }
}
