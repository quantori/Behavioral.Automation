using Behavioral.Automation.Services.Mapping.Contract;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Elements' captions and locators are stored here
    /// </summary>
    [UsedImplicitly]
    public abstract class UserInterfaceBuilderBase : IUserInterfaceBuilder
    {
        protected IScopeMarkupMapper Mapper { get; }

        protected UserInterfaceBuilderBase([NotNull] IScopeMarkupMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Build web elements pipes
        /// </summary>
        public virtual void Build()
        {
        }
    }
}
