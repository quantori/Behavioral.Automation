using Behavioral.Automation.Services.Mapping.Contract;

namespace Behavioral.Automation.Services.Mapping
{
    public class ScopeMappingPipe : IScopeMappingPipe
    {
        private readonly IMarkupStorageInitializer _markupStorage;

        public ScopeMappingPipe(IMarkupStorageInitializer markupStorage)
        {
            _markupStorage = markupStorage;
        }

        public IHtmlTagMapper Register(string tag)
        {
            return new HtmlTagMapper(_markupStorage, tag);
        }

        public IScopeMappingPipe CreateControlMappingPipe(ControlScopeId controlScopeId, ControlScopeOptions controlScopeOptions = null)
        {
            return new ScopeMappingPipe(_markupStorage.GetOrCreateControlScopeMarkupStorage(controlScopeId, controlScopeOptions));
        }

        public IScopeMappingPipe CreateControlMappingPipe(string controlScopeId, bool isVirtualized = false)
        {
            return CreateControlMappingPipe(new ControlScopeId(controlScopeId), new ControlScopeOptions(isVirtualized));
        }


        public void Dispose()
        {
           
        }
    }
}