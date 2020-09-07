using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class NewsViewModel
    {
        public string Title { get; set; }
        public string  Author { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string UrlToImg { get; set; }
    }
}
