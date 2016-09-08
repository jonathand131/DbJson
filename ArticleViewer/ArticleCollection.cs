using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArticleViewer
{
    class ArticlesCollection
    {
        private List<Article> articles;

        public ArticlesCollection()
        {
            articles = new List<Article>();
        }

        public ReadOnlyCollection<Article> Articles
        {
            get
            {
                return this.articles.AsReadOnly();
            }
        }

        public void AddArticle(Article article)
        {
            articles.Add(article);
        }
    }
}
