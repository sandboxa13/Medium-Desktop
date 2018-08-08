using System;
using System.IO;
using System.Net;
using System.Text;
using Medium.Domain.Domain;

namespace Medium.Domain.Extensions
{
    public static class WebRequestExtensions
    {
        public static T GetResponseJson<T>(this WebRequest request) where T : class
        {
            return request.GetResponse(Newtonsoft.Json.JsonConvert.DeserializeObject<JsonResponse<T>>)?.Data;
        }

        public static T GetResponse<T>(
            this WebRequest request,
            Func<string, T> responseBodyParser)
        {
            try
            {
                using (var response = request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream == null)
                        return default(T);

                    return responseBodyParser(responseStream.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                var responseStream = ex.Response?.GetResponseStream();
                if (responseStream != null)
                {
                    var responseBody = responseStream.ReadToEnd();
                    throw new InvalidOperationException(responseBody, ex);
                }
                throw;
            }
        }

        public static string ReadToEnd(
            this Stream stream,
            bool seekToStart = true,
            Encoding encoding = null)
        {
            if (stream.CanSeek && seekToStart)
                stream.Seek(0, SeekOrigin.Begin);

            return (encoding == null ? new StreamReader(stream) : new StreamReader(stream, encoding))
                .ReadToEnd();
        }
    }
}
