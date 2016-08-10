using System;
using System.Data.Common;

namespace DbJsonServer
{
    class Article
    {
        public readonly Int64 id;
        public readonly string title;
        public readonly string subTitle;
        public readonly string author;
        public readonly string content;
        public readonly DateTime dateTime;

        public Article(DbDataReader reader)
        {
            id = reader.GetInt64(reader.GetOrdinal("id"));
            title = reader.GetString(reader.GetOrdinal("title"));
            subTitle = reader.GetString(reader.GetOrdinal("sub_title"));
            author = reader.GetString(reader.GetOrdinal("author"));
            dateTime = reader.GetDateTime(reader.GetOrdinal("date"));
            try
            {
                content = reader.GetString(reader.GetOrdinal("content"));
            }
            catch (IndexOutOfRangeException)
            {
                content = null;
            }
        }
    }
}
