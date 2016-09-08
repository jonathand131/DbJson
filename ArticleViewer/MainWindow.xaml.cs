using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArticleViewer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ArticlesLoader loader = (ArticlesLoader)Application.Current.Properties["articlesLoader"];
            this.MasterContainer.DataContext = new ArticlesViewModel(loader);
            loader.LoadAsync();

            Browser.LoadCompleted += this.SetBrowserStylesheet;
        }

        protected void SetBrowserStylesheet(object sender, NavigationEventArgs e)
        {
            HTMLDocument doc = (Browser.Document) as HTMLDocument;
            // The first parameter is the url, the second is the index of the added style sheet.
            IHTMLStyleSheet ss = doc.createStyleSheet("", 0);
            //ss.addRule(".actu_chapeau", "border-style: dotted; border-width: 1px; border-color: red; padding= 10px;");
            ss.cssText = @"
.actu_title {
    display: block
}
.actu_icons {
	margin-top:3px
    }
.actu_title {
	font-weight:bold;
	font-size:14px
}
.actu_title_icons_collumn {
	display:table-cell;
	width:34px;
	margin-right:12px;
	vertical-align:middle;
	height:100%;
	padding:0 5px
}
.actu_title_collumn {
	display:table-cell;
	vertical-align:top;
	padding:5px 0 5px 10px
}
.actu_sub_title {
	font-weight:normal;
	font-size:16px
}
.actu_title h1
{
    font-size:18px;
    margin:0 0 3px 0;
    font-family:""Ubuntu Condensed"",""Ubuntu"",""Arial"";
    padding:0;
    font-weight:normal
}
.actu_content {
	padding:8px!important;
	padding-top:10px;
	text-align:justify;
	font-size:16px;
	line-height:20px
}
.actu_content ul,.actu_content ol
{
    margin:1em 0 1em 24px;
    list-style-type:disc;
    padding:0
}
.actu_content figure
{
    margin:12px;
    text-align:center
}
figure figcaption
{
    font-size:10px;
    font-weight:bold
}
.actu_content h3,.intertitre {
	font-size:18px;
	margin-bottom:14px;
	margin-top:14px;
	font-family:""Ubuntu COndensed"";
	text-align:left
}
.actu_content p
{
    margin-bottom:15px
}
.ui-select.ui-mini.ui-btn-icon-right.ui-btn-inner {
	padding-right:32px;
	padding-top:11px
}
.actu_chapeau a
{
    font-weight:bold!important
}
.actu_chapeau {
	font-weight:bold;
}
code
{
    border:1px solid #ccc;
	display:block;
    font-family:monospace;
    margin-bottom:5px;
    margin-top:5px;
    padding:5px;
    text-align:left
}
code
{
    background:none repeat scroll 0 0 #f4f4f4
}
.actu_footer {
	font-style:italic;
	font-size:12px;
	padding:6px
}
.actu_footer a
{
    text-decoration:none;
    color:#df8d18
}";
        }
    }
}
