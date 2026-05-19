using Stuff802.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Stuff802.Core.Interfaces
{
    public interface ISearchService
    {
        SearchResultsModel GetSearchResults(string searchTerm, int page, SearchPage searchPage);
    }
}
