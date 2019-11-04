using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace EventApp.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private IApiLogService _apiLogService;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiLogService apiLogService)
        {
            _apiLogService = apiLogService;

            var request = context.Request;
            if (request.Path.StartsWithSegments(new PathString("/api")))
            {
                var stopWatch = Stopwatch.StartNew();
                var requestTime = DateTime.UtcNow;

                var requestBodyContent = await GetRequestBodyContent(context.Request);
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    var response = context.Response;
                    response.Body = responseBody;

                    await _next(context);

                    stopWatch.Stop();

                    var responseBodyContent = await GetResponseBodyContent(context.Response);
                        
                    var userName = context.User.Identity.Name;
                    await SafeLog(userName,
                    requestTime,
                        stopWatch.ElapsedMilliseconds,
                        response.StatusCode,
                        request.Method,
                        request.Path,
                        request.QueryString.ToString(),
                        requestBodyContent,
                        responseBodyContent);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task<string> GetRequestBodyContent(HttpRequest request)
        {
            request.EnableRewind();
            
            var bodyText = await new StreamReader(request.Body).ReadToEndAsync();

            request.Body.Position = 0;

            return bodyText;
        }

        private async Task<string> GetResponseBodyContent(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            
            string bodyText = await new StreamReader(response.Body).ReadToEndAsync();
            
            response.Body.Seek(0, SeekOrigin.Begin);
            
            return bodyText;
        }

        private async Task SafeLog(string userName,
            DateTime requestTime,
            long responseMillis,
            int statusCode,
            string method,
            string path,
            string queryString,
            string requestBody,
            string responseBody)
        {
            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                if (path.ToLower().StartsWith("/api/auth/login"))
                {
                    requestBody = "(Request logging disabled for /api/auth/login)";
                    responseBody = "(Response logging disabled for /api/auth/login)";
                }
                else
                {
                    if (requestBody.Length > 5000)
                    {
                        requestBody = $"(Truncated to 5000 chars) {requestBody.Substring(0, 5000)}";
                    }

                    if (responseBody.Length > 5000)
                    {
                        responseBody = $"(Truncated to 5000 chars) {responseBody.Substring(0, 5000)}";
                    }
                }
                
                if (queryString.Length > 1000)
                {
                    queryString = $"(Truncated to 1000 chars) {queryString.Substring(0, 1000)}";
                }

                _apiLogService.Log(new ApiLogEntry
                {
                    UserName = userName,
                    RequestTime = requestTime,
                    ResponseMillis = responseMillis,
                    StatusCode = statusCode,
                    Method = method,
                    Path = path,
                    QueryString = queryString,
                    RequestBody = requestBody,
                    ResponseBody = responseBody
                });
            }

            
        }
    }
}
