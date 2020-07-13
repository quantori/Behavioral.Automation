using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public abstract class UserInterfaceBuilderBase : IUserInterfaceBuilder
    {
        protected IScopeMarkupMapper Mapper { get; }

        protected UserInterfaceBuilderBase([NotNull] IScopeMarkupMapper mapper)
        {
            Mapper = mapper;
        }

        public virtual void Build()
        {
        }
    }
}
