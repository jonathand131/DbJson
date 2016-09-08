using System;
using System.Windows;
using System.Windows.Controls;

namespace ArticleViewer
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(Article), typeof(WebBrowserUtility), new PropertyMetadata(ContentPropertyChanged));

        public static Article GetContent(DependencyObject obj)
        {
            return (Article)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, Article value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static void ContentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                Article article = e.NewValue as Article;
                if (article.Content != null)
                {
                    browser.NavigateToString(article.Content);
                }
                else
                {
                    browser.NavigateToString("Loading...");
                }
            }
        }
    }
}
