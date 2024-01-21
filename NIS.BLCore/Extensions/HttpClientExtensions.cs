using log4net;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NIS.BLCore.Extensions
{
    public static class HttpClientExtentions
    {
        public static string PostAsBasicAuth(IConfiguration configuration, int? CustomerRequestId, int? status=1)
        {
            // create a logger 
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            var username = "aztelekom";
            var password = "c89a8T";
            //var status = "İcrada";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                username,
                requestId=CustomerRequestId,
                password,
                status
            });
            logger.Info(
               "Post orangline process started with this parameters: " + json
              + $"{Environment.NewLine}"
              + " - Executed method : PostAsBasicAuth");

            ////Set Basic Auth
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            var parametrs = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            var aclient = new RestClient(configuration["OrangLine"]);
            MultipartFormDataContent form = new MultipartFormDataContent();

            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic " + base64String);
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW",
              "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"status\"\r\n\r\n" + status.ToString() + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data;" +
              " name=\"description\"\r\n\r\ntest description\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; " +
              "name=\"id\"\r\n\r\n" + CustomerRequestId + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = aclient.Execute(request);
            logger.Info(
             "Post orangline process endded with this parameters: " + response.Content
            + $"{Environment.NewLine}"
            + " - Executed method : PostAsBasicAuth");
            return response.Content;

        }
    }
}
