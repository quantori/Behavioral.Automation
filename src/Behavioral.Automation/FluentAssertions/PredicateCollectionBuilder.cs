using Behavioral.Automation.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Behavioral.Automation.FluentAssertions
{
    public class PredicateCollectionBuilder
    {
        private readonly IWebElementWrapper _element;
        private List<Func<bool>> _predicates = new List<Func<bool>>();

        private PredicateCollectionBuilder(IWebElementWrapper element)
        {
            _element = element;
        }

        public static PredicateCollectionBuilder For(IWebElementWrapper element) =>
            new PredicateCollectionBuilder(element);

        public PredicateCollectionBuilder UsePredicate(Func<bool> predicate)
        {
            _predicates.Add(predicate);
            return this;
        }

        public PredicateCollectionBuilder UsePredicate(Func<IWebElementWrapper, Func<bool>> predicateFactoryFn)
        {
            _predicates.Add(predicateFactoryFn(_element));
            return this;
        }

        public Func<bool>[] ToArray() =>
            _predicates.ToArray();
    }
}
