using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace ArticleViewer
{
    [DataContract]
    public class Article
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Int64 Id { get; set; }
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
        [DataMember(Name = "subTitle", EmitDefaultValue = false)]
        public string SubTitle { get; set; }
        [DataMember(Name = "author", EmitDefaultValue = false)]
        public string Author { get; set; }
        [DataMember(Name = "content", EmitDefaultValue = false)]
        public string Content { get; set; }
        [DataMember(Name = "dateTime", EmitDefaultValue = false)]
        public DateTime DateTime { get; set; }

        public Article(Int64 id, string title, string subTitle, string author, string content, DateTime dateTime)
        {
            Id = id;
            Title = title;
            SubTitle = subTitle;
            Author = author;
            Content = content;
            DateTime = dateTime;
        }

        public Article()
        {
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
