using System.ServiceProcess;

namespace ArticleDbService
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DbJsonHttpService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
