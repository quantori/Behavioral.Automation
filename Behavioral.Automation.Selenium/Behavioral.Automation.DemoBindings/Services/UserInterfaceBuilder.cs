using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation.DemoBindings
{
    public partial class UserInterfaceBuilder : UserInterfaceBuilderBase
    {
        public UserInterfaceBuilder(IScopeMarkupMapper mapper)
            : base(mapper)
        {

        }

        public override void Build()
        {
            using (var mappingPipe = Mapper.GetGlobalMappingPipe())
            {
                mappingPipe.Register("header").Alias("header")
                    .With("main-page-header").As("Main page")
                    .With("main-elements-list-header").As("Elements list")
                    .With("greetings-page-header").As("Greetings page")
                    .With("counter-page-header").As("Counter page")
                    .With("forecast-page-header").As("Weather forecast page");

                mappingPipe.Register("text").Alias("text")
                    .With("main-page-paragraph").As("Main page")
                    .With("greetings-input-text").As("Enter your name")
                    .With("counter-page-text").As("Current count")
                    .With("forecast-page-paragraph").As("Forecast page");

                mappingPipe.Register("table").Alias("table")
                    .With("forecast-table").As("Forecast");

                mappingPipe.Register("list").Alias("list")
                    .With("main-elements-list").As("Main elements");

                mappingPipe.Register("link").Alias("link")
                    .With("nav-link-home").As("Homepage")
                    .With("nav-link-greetings").As("Greetings")
                    .With("nav-link-counter").As("Counter")
                    .With("nav-link-forecast").As("Forecast");

                mappingPipe.Register("button").Alias("button")
                    .With("greetings-page-button").As("Say hello")
                    .With("counter-page-button").As("Click me");

                mappingPipe.Register("field").Alias("field")
                    .With("greetings-page-input").As("Name");
            }
        }
    }
}
