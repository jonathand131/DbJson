using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ArticleViewer
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ArticlesLoader loader;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            loader = new ArticlesLoader();
            this.Properties["articlesLoader"] = loader;
        }
    }
}
