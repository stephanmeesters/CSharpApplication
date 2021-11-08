using System;
using System.Collections.Generic;

namespace AvaloniaTest.PLOSOne
{
    

    // level 0
    public class Response
    {
        public ResponseBody response { get; set; }
    }

    // level 1
    public class ResponseBody
    {
        public long numFound { get; set; }
        public long start { get; set; }
        public double maxScore { get; set; }
        public Doc[]? docs { get; set; }
    }

    // level 2
    public class Doc
    {
        public string id { get; set; }
        public string journal { get; set; }
        public string eissn { get; set; }
        public string publication_date { get; set; }
        public string article_type { get; set; }
        public string[]? author_display { get; set; }
        public string[]? @abstract { get; set; }
        public string title_display { get; set; }
        public float score { get; set; }

        public string short_title
        {
            get
            {
                return Utils.Truncate(title_display, 42);
            }
        }

        public string publication_date_formatted
        {
            get
            {
                return Utils.FormatISO8602(publication_date);
            }
        }
    }
}