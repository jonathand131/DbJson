using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Threading;

namespace ArticleViewer
{
    public delegate void ArticlesListLoadedEventHandler(object sender);

    class ArticlesLoader
    {
        public event ArticlesListLoadedEventHandler ArticlesListLoaded;

        private List<Article> articles;

        public ArticlesLoader()
        {
            this.articles = new List<Article>();
        }

        public ReadOnlyCollection<Article> Articles
        {
            get
            {
                return this.articles.AsReadOnly();
            }
        }

        public void LoadAsync()
        {
            Task.Run(() => { this.Load(); });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            // Get articles list
            HttpClient httpClient = new HttpClient();
            Task<Stream> streamAsync = httpClient.GetStreamAsync("http://localhost/articles");
            // TODO: handle loading errors (connexion error, timeout, ...)

            // Decode JSON
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Article>));
            List<Article> articlesList = (List<Article>)ser.ReadObject(streamAsync.Result);

            foreach (Article article in articlesList)
            {
                articles.Add(article);
            }

            this.OnArticlesListLoaded();
        }

        public void LoadArticle(int position)
        {
            Article currentArticle = articles[position];
            if (currentArticle.Content != null)
                return;

            // Get full article
            HttpClient httpClient = new HttpClient();
            Task<Stream> streamAsync = httpClient.GetStreamAsync("http://localhost/articles?id=" + currentArticle.Id.ToString());
            // TODO: handle loading errors (connexion error, timeout, ...)

            // Decode JSON
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Article>));
            List<Article> articlesList = (List<Article>)ser.ReadObject(streamAsync.Result);
            currentArticle.Content = articlesList[0].Content;
            Console.WriteLine("Article " + currentArticle.Id.ToString() + " loaded\n");
        }

        private void OnArticlesListLoaded()
        {
            if (ArticlesListLoaded != null)
            {
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    ArticlesListLoaded(this);
                }
                else
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() => { ArticlesListLoaded(this); }));
                }
            }
        }
    }
}
