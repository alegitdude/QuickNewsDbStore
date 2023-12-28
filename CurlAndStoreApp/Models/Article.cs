using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CurlAndStore.Models
{
    public class Article
    {
        public Article(string uuid, string title, string description, string keywords, 
            string snippet, string url, string image_Url, string language, 
            DateTime published_At, string source, string category1, string category2,
            double? relevance_Score, string locale, int topStory)
        {
            Uuid = uuid;
            Title = title;
            Description = description;
            Keywords = keywords;
            Snippet = snippet;
            Url = url;
            Image_Url = image_Url;
            Language = language;
            Published_At = published_At;
            Source = source;
            Category1 = category1;
            Category2 = category2;
            Relevance_Score = relevance_Score;
            Locale = locale;
            TopStory = topStory;
        }

        [Key]
        public string Uuid { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [StringLength(1000)]
        public string Keywords { get; set; } = string.Empty;
        [StringLength(4000)]
        public string Snippet { get; set; } = string.Empty;
        [StringLength(100)]
        public string Url { get; set; } = string.Empty;
        [StringLength(200)]
        public string Image_Url { get; set; } = string.Empty;
        [StringLength(20)]
        public string Language { get; set; } = string.Empty;
        public DateTime Published_At { get; set; } = DateTime.MinValue;
        [StringLength(100)]
        public string Source { get; set; } = string.Empty;
        [StringLength(20)]
        public string Category1 { get; set; } = string.Empty;
        [StringLength(20)]
        public string? Category2 { get; set; } = string.Empty;
        public double? Relevance_Score { get; set; } = null;
        [StringLength(100)]
        public string Locale { get; set; } = string.Empty;
        public int TopStory { get; set; } = 1;





    }
}































//{
//    public class Article
//    {
//        public Article(string uuid, string title, string description, string keywords, 
//            string snippet, string url, string image_Url, string language, DateTime published_At, 
//            string source, IReadOnlyList<string> categories, double? relevance_Score, string locale)
//        {
//            Uuid = uuid;
//            Title = title;
//            Description = description;
//            Keywords = keywords;
//            Snippet = snippet;
//            Url = url;
//            Image_Url = image_Url;
//            Language = language;
//            Published_At = published_At;
//            Source = source;
//            Categories = categories;
//            Relevance_Score = relevance_Score;
//            Locale = locale;
//        }

//        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
//        public ObjectId _id { get; set; }

//        [BsonElement("uuid")]
//        public string Uuid { get; set; } = string.Empty;

//        [BsonElement("title")]
//        public string Title { get; set; } = string.Empty;

//        [BsonElement("description")]
//        public string Description { get; set; } = string.Empty;

//        [BsonElement("keywords")]
//        public string Keywords { get; set; } = string.Empty;

//        [BsonElement("snippet")]
//        public string Snippet { get; set; } = string.Empty;

//        [BsonElement("url")]
//        public string Url { get; set; } = string.Empty;

//        [BsonElement("image_url")]
//        public string Image_Url { get; set; } = string.Empty;

//        [BsonElement("language")]
//        public string Language { get; set; } = string.Empty;

//        [BsonElement("published_at")]
//        public DateTime Published_At { get; set; } = DateTime.MinValue;

//        [BsonElement("source")]
//        public string Source { get; set; } = string.Empty;

//        [BsonElement("categories")]
//        public IReadOnlyList<string> Categories { get; set; } = new List<string>();

//        [BsonElement("relevance_score")]
//        public double? Relevance_Score { get; set; } = null;

//        [BsonElement("locale")]
//        public string Locale { get; set; } = string.Empty;

       

//    }
//}