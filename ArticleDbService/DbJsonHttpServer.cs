using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace DbJsonServer
{
    class DbJsonHttpServer : HttpServer
    {
        private static readonly string[] supportedHttpMethods = { "GET" };
        private DbAccessor dbAccessor;

        public DbJsonHttpServer(string[] prefixes, DbAccessor db) : base(prefixes)
        {
            dbAccessor = db;
        }

        override
        protected void HandleRequest(object obj_context)
        {
            var context = obj_context as HttpListenerContext;
            HttpListenerRequest request = context.Request;

            // Set default status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Check HTTP method validity
            if (!supportedHttpMethods.Contains(request.HttpMethod))
            {
                respondWithError(context, (int)HttpStatusCode.MethodNotAllowed, "Unsupported HTTP method");
                return;
            }

            // Check request details
            if (!IsAcceptable(request))
            {
                respondWithError(context, (int)HttpStatusCode.NotAcceptable, "Can only serve application/json");
                return;
            }

            // Handle request
            List<Article> dbResults = null;
            if (request.Url.Segments[1].Equals("articles"))
            {
                if (request.QueryString.Count > 0)
                {
                    // Return full articles based on criteria
                    string id = request.QueryString.Get("id");
                    string dateStart = request.QueryString.Get("date_range_start");
                    string dateEnd = request.QueryString.Get("date_range_end");
                    if (id != null)
                    {
                        dbResults = dbAccessor.GetArticlesById(Convert.ToInt64(id));
                    }
                    else if ((dateStart != null) && (dateEnd != null))
                    {
                        try
                        {
                            // Parse date range
                            DateTime dtStart = DateTime.Parse(dateStart);
                            DateTime dtEnd = DateTime.Parse(dateEnd);

                            // Query database
                            dbResults = dbAccessor.GetArticlesByDateRange(dtStart, dtEnd);
                        }
                        catch (FormatException)
                        {
                            respondWithError(context, (int)HttpStatusCode.BadRequest, "Unable to parse date range");
                            return;
                        }
                    }
                    else
                    {
                        respondWithError(context, (int)HttpStatusCode.BadRequest, "Expect id or date range");
                    }
                }
                else
                {
                    // Return a simple list of last articles
                    dbResults = dbAccessor.GetLastArticlesList();
                }
            }
            else
            {
                respondWithError(context, (int)HttpStatusCode.BadRequest, "Unsupported query");
                return;
            }

            if (dbResults != null)
            {
                // Convert results to Json
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                string jsonString = jsSerializer.Serialize(dbResults);

                // Populate response
                byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.ContentLength64 = buffer.Length;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }

            context.Response.OutputStream.Close();
            context.Response.Close();
        }

        private void respondWithError(HttpListenerContext context, int errorCode, string errorMsg)
        {
            // Generate error page
            StringBuilder message = new StringBuilder();
            message.AppendLine("<!DOCTYPE html>");
            message.AppendLine("<html>");
            message.AppendLine("<head>");
            message.AppendLine("<meta charset=\"utf-8\">");
            message.AppendFormat("<title>{0} - {1}</title>\n", errorCode, errorMsg);
            message.AppendLine("</head><body>");
            message.AppendFormat("<h1>{0}</h1>\n", errorCode);
            message.AppendFormat("<p>{0}</p>\n", errorMsg);
            message.AppendLine("</body></html>");

            // Populate response
            byte[] buffer = Encoding.UTF8.GetBytes(message.ToString());
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = buffer.Length;
            context.Response.StatusCode = errorCode;
            if (errorMsg != null)
            {
                context.Response.StatusDescription = errorMsg;
            }
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
            context.Response.Close();
        }

        private bool IsAcceptable(HttpListenerRequest request)
        {
            foreach (string param in request.AcceptTypes)
            {
                string[] splittedParam = param.Split(';');
                if ((splittedParam[0].Equals("application/json")) ||
                    (splittedParam[0].Equals("application/*")) ||
                    (splittedParam[0].Equals("*/*")))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
