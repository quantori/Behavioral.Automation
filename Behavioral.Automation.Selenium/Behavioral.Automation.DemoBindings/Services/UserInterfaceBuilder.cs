using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation.DemoBindings
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
                mappingPipe.Register("label").Alias("label")
                    .With("main-page-header").As("Demo");
            }
        }
    }
}
