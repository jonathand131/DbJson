using System.Net;
using System.Threading;

namespace DbJsonServer
{
    abstract class HttpServer
    {
        protected HttpListener listener = new HttpListener();

        public HttpServer(string[] prefixes)
        {
            // Setup HTTP listener
            foreach(string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            listener.Start();
        }

        public void Run()
        {
            // Create a thread to serve requests
            ThreadPool.QueueUserWorkItem(HandleRequests);
        }

        public void Close()
        {
            listener.Stop();
            listener.Close();
        }

        private void HandleRequests(object state)
        {
            // Loop to handle incoming requests
            while(listener.IsListening)
            {
                try
                {
                    // Wait and retrieve a new request
                    HttpListenerContext context = listener.GetContext();

                    // Handle the request in a separate thread
                    ThreadPool.QueueUserWorkItem(HandleRequest, context);
                } catch { }
            }
        }

        protected abstract void HandleRequest(object obj_context);
    }
}
