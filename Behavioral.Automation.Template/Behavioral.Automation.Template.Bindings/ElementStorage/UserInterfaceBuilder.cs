using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation.Template.Bindings.ElementStorage
{
    public class UserInterfaceBuilder : UserInterfaceBuilderBase
    {
        public UserInterfaceBuilder(IScopeMarkupMapper mapper)
            : base(mapper)
        {

        }

        public override void Build()
        {
            using (var mappingPipe = Mapper.GetGlobalMappingPipe())
            {
                mappingPipe.Register("input").Alias("input")
                    .With("searchInput").As("Search");

                mappingPipe.Register("input").Alias("button")
                    .With("searchButton").As("Magnifying glass");

                mappingPipe.Register("h1")
                    .With("firstHeading").As("Page header");
            }
        }
    }
}
