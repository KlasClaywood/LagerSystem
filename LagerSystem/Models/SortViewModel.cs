using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LagerSystem.Models
{
    public class SortViewModel
    {
        public string SortVariable { get; set; }
        public bool Descending { get; set; }

        public SortViewModel()
        {

        }
        public SortViewModel(string sort, bool desc)
        {
            SortVariable = sort;
            Descending = desc;
        }
    }
}