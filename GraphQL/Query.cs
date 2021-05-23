using HotChocolate;
using HotChocolate.Data;
using System.Linq;
using System.Threading.Tasks;
using TitleInfoGQL.Data;
using TitleInfoGQL.Models;

namespace TitleInfoGQL.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {
        [UseDbContext(typeof(TitlesContext))]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the list of titles.")]
        public IQueryable<Title> GetTitles([ScopedService] TitlesContext context)
        {
            return context.Titles;
        }
    }
}
