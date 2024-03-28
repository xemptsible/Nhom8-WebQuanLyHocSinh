using QLHS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHS.Patterns.Strategy
{
    public class SearchContext
    {
        private ISearchStrategy _strategy;

        public SearchContext() { }

        public SearchContext(ISearchStrategy strategy) 
        {
            this._strategy = strategy;
        }

        public void SetSearchType(ISearchStrategy searchStrategy)
        {
            this._strategy = searchStrategy;
        }

        public List<HocSinh> Searching(string searchString, QLDEntities db)
        {
            return _strategy.SearchStrategy(searchString, db);
        }
    }
}