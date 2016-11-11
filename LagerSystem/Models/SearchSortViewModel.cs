using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LagerSystem.Models
{
    public class SearchSortViewModel : SortViewModel
    {
        public string SearchName { get; set; }
        public decimal? SearchPrice { get; set; }
        public string SearchShelf { get; set; }
        public string SearchDescription { get; set; }

        public SearchSortViewModel() : base()
        {

        }
        public SearchSortViewModel(SearchSortViewModel prototype)
        {
            SearchName = prototype.SearchName;
            SearchPrice = prototype.SearchPrice;
            SearchShelf = prototype.SearchShelf;
            SearchDescription = prototype.SearchDescription;
            SortVariable = prototype.SortVariable;
            Descending = prototype.Descending;
        }
        public static SearchSortViewModel ReverseSorter ( SearchSortViewModel target )
        {
            target.Descending = !target.Descending;
            return target;
        }
    }
}