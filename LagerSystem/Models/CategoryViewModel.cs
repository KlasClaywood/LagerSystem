using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LagerSystem.Models
{
    public class CategoryViewModel
    {
        [Key]
        public string Name { get; set; }
        public int ContentCount { get; set; }
    }
}