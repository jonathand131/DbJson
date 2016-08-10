using DbJsonServer;
using System.ServiceProcess;

namespace ArticleDbService
{
    public partial class DbJsonHttpService : ServiceBase
    {
        private DbAccessor dbAccessor;
        private DbJsonHttpServer dbJsonHttpServer;

        public DbJsonHttpService()
        {
            InitializeComponent();
            
            dbAccessor = new DbAccessor();
            dbJsonHttpServer = new DbJsonHttpServer(new string[] { "http://localhost/" }, dbAccessor);
        }

        protected override void OnStart(string[] args)
        {
            dbJsonHttpServer.Run();
        }

        protected override void OnStop()
        {
            dbJsonHttpServer.Close();
        }
    }
}
