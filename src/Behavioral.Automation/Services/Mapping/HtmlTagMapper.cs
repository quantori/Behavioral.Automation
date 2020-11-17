using JetBrains.Annotations;

namespace Behavioral.Automation.Services.Mapping
{
    public class HtmlTagMapper : IHtmlTagMapper
    {
        private readonly IMarkupStorageInitializer _storage;
        private readonly string _htmlTag;

        public HtmlTagMapper([NotNull] IMarkupStorageInitializer storage, [NotNull] string htmlTag)
        {
            _storage = storage;
            _htmlTag = htmlTag;
            _storage.StartCreationOfNewComposition();
        }

        public IHtmlTagMapper Alias(string alias)
        {
            _storage.AddAlias(_htmlTag, alias);
            return this;
        }

        public IControlMap With(string id, string subpath = null)
        {
            return new ControlMap(_storage, this, _htmlTag, id, subpath);
        }

        public IControlMap WithPath(string path)
        {
            return  new ControlMap(_storage, this, _htmlTag, null,path);
        }
    }
}