namespace Behavioral.Automation.Services.Mapping
{
    public class ControlMap : IControlMap
    {
        private readonly IMarkupStorageInitializer _storage;
        private readonly IHtmlTagMapper _tagMapper;
        private readonly string _htmlTag;
        private readonly string _id;
        private readonly string _subpath;

        public ControlMap(
            IMarkupStorageInitializer storage, 
            IHtmlTagMapper tagMapper, 
            string htmlTag, 
            string id,
            string subpath = null)
        {
            _storage = storage;
            _tagMapper = tagMapper;
            _htmlTag = htmlTag;
            _id = id;
            _subpath = subpath;
        }

        public IHtmlTagMapper As(string caption)
        {
            _storage.AddComposition(_htmlTag, _id, caption, _subpath);
            return _tagMapper;
        }
    }
}