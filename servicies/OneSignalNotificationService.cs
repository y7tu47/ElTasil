using Microsoft.AspNetCore.Mvc;
using Sentry.Protocol;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ElTasilAcademy.servicies
{
    public class OneSignalNotificationService 
    {
        private string ApiKey = "os_v2_app_fyzzgitq3bb6rar7qcq7jr3j7s75j3pzjraucmf6y73w23powgg23ih4kas2a43432ypoasmxofd2lqpre6kmqvdb2umk3ywkc7blmy";
        private string AppId = "2e339322-70d8-43e8-823f-80a1f4c769fc";
        private string ApiUrl = "https://api.onesignal.com/notifications";
        public void send()
        {
            try
            {
                var webRequest = WebRequest.Create($"{ApiUrl}") as HttpWebRequest;
                webRequest.KeepAlive = true;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Headers.Add("authorization", $"Basic {ApiKey}");

                var obj = new
                {
                    app_id = AppId,
                    headings = new { en = "Web Push Notification Title" }, //this value can be change as per need, can be a value from db
                    contents = new { en = "Here it goes push notification content" }, //this value can be change as per need, can be a value from db
                    included_segments = new string[] { "All" },
                    url = ""
                };
                var param = JsonSerializer.Serialize(obj);
                var byteArray = Encoding.UTF8.GetBytes(param);

                using (var writer = webRequest.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        using var reader = new StreamReader(response.GetResponseStream());
                        var responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
