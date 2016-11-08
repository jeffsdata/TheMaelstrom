using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class Articles
    {
        [JsonProperty("items")]
        public IList<Article> articles{ get; set; }
    }

    public class Article
    {
        public ArticleSystem system {get; set;}
        public ArticleElements elements { get; set; }
    }


    public class ArticleSystem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string codename { get; set; }
        public string type { get; set; }
        public IList<string> sitemap_locations { get; set; }
        public DateTime last_modified { get; set; }

    }

    public class ArticleElements
    {
        public ArticleTitle title {get; set;}
        public ArticleAsset full_width_image { get; set; }
    }

    public class ArticleTitle
    {
        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class ArticleAsset
    {
        public string type { get; set; }
        public string name { get; set; }
        public IList<AssetDetails> value { get; set; }
    }

    public class AssetDetails
    {
        public string name { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public string url { get; set; }
    }
}
