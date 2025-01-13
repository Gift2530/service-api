using ProductData.Common;
using System.Text;

namespace ProductData
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        string systemName = $"service_api";
        private readonly IGlobalErrorInstance globalErrorInstance;

        public ExceptionHandler(RequestDelegate next, IGlobalErrorInstance globalErrorInstance)
        {
            _next = next;
            this.globalErrorInstance = globalErrorInstance;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment webHostEnvironment)
        {
            string requestBody = String.Empty;
            try
            {
                requestBody = ExpandRequest(context);
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, webHostEnvironment.EnvironmentName, requestBody);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string envName, string requestBody)
        {

        }
        private string ExpandRequest(HttpContext context)
        {
            string Body = String.Empty;
            HttpRequest request = context.Request;
            if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                HttpRequestRewindExtensions.EnableBuffering(request);
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                request.Body.Position = 0;
                Body = bodyAsText;
            }
            else
            {
                if (!(request.ContentType is null) && !request.ContentType.Contains("multipart/form-data"))
                {
                    HttpRequestRewindExtensions.EnableBuffering(request);
                    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    request.Body.Position = 0;
                    Body = bodyAsText;
                }
            }

            return Body;
        }
    }
}
