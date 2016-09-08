using System;
using System.Collections.Specialized;
using System.Windows.Data;

namespace ArticleViewer
{
    class ArticlesCollectionView : ListCollectionView
    {
        private ArticlesLoader loader;

        public ArticlesCollectionView(ArticlesLoader loader) : base(loader.Articles)
        {
            this.loader = loader;
            loader.ArticlesListLoaded += Loader_ArticlesListLoaded;
            this.CurrentChanged += this.LoadArticleContent;
        }

        private void Loader_ArticlesListLoaded(object sender)
        {
            ArticlesLoader loader = sender as ArticlesLoader;
            this.OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
        }

        private void LoadArticleContent(object sender, EventArgs e)
        {
            if (this.CurrentPosition == -1)
                return;

            loader.LoadArticle(this.CurrentPosition);
        }
    }

    class ArticlesViewModel
    {
        private ArticlesCollectionView articlesView;

        public ArticlesViewModel(ArticlesLoader articlesLoader)
        {
            articlesView = new ArticlesCollectionView(articlesLoader);
        }

        public CollectionView Articles
        {
            get
            {
                return articlesView;
            }
        }
    }
}
