using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Authentication.OnlineId;

namespace LightFun.Models
{
    class HueCommunicationService
    {
        private string userId;
        private string hueUri = @"http://192.168.1.253/api/"; //TODO: Un-hardcode this

        public HueCommunicationService()
        {
            userId = GetHueUserId();
        }

        public string GetHueUserId()
        {
            string apiKey = Environment.GetEnvironmentVariable("HUE_API_KEY");
            // Stub for now
            // TODO: Add in logic that will generate a user ID
            // if it has not already been created - also 
            // will want to persist user ID to disk/database
            // for later retrieval after app closes
            return apiKey;
        }

        public List<LightItem> GetAvailableLights()
        {
            List<LightItem> availableLights = new List<LightItem>();
            string html = string.Empty;
            string uri = hueUri + userId + "/lights";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            var jsonResponse = JsonObject.Parse(html);
            foreach (var key in jsonResponse)
            {
                int lightNum = int.Parse(key.Key);
                JsonObject jsonObject = key.Value.GetObject();
                string lightName = jsonObject.GetNamedString("name");
                availableLights.Add(new LightItem(lightName, lightNum));
            }

            return availableLights;
        }

        public void ChangeLightColor(ColorDTO color, int lightNum)
        {
            string uri = hueUri + userId + "/lights/" + lightNum + "/state";

            double[] colorAsXY = ConvertToColorXY(color);
            int hue = hueFromRGB(color);

            string data = "{ \"on\": true, \"sat\": 254, \"bri\": 254, \"hue\":" 
                + hue +
                ",\"xy\":" + "[" + colorAsXY[0] + ", " + colorAsXY[1] + "], \"effect\": \"none\"}";

            DoJSONPutRequest(uri, data);
        }

        public void CycleLightColor(ColorDTO color, int lightNum)
        {
            string uri = hueUri + userId + "/lights/" + lightNum + "/state";
           
            double[] colorAsXY = ConvertToColorXY(color);
            int hue = hueFromRGB(color);

            string data = "{ \"on\": true, \"sat\": 254, \"bri\": 254, \"hue\":"
                + hue +
                ",\"xy\":" + "[" + colorAsXY[0] + ", " + colorAsXY[1] + "], \"effect\": \"colorloop\" }";

            DoJSONPutRequest(uri, data);
        }

        private void DoJSONPutRequest(string uri, string jsonBody)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = "PUT";
            request.ContentType = "application/json";


            StreamWriter dataStream = new StreamWriter(request.GetRequestStream());
            dataStream.Write(jsonBody);
            dataStream.Close();

            WebResponse response = request.GetResponse();

            Stream responseStream;
            using (responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream);
                string responseFromServer = reader.ReadToEnd();

                Debug.WriteLine(responseFromServer);
            }

            response.Close();
        }

        private double[] ConvertToColorXY(ColorDTO color)
        {
            double[] xy = new double[2];
            double r = color.r / 254.0;
            double g = color.g / 254.0;
            double b = color.b / 254.0;

            r = (r > 0.04045) ? Math.Pow((r + 0.055) / (1.0f + 0.055), 2.4) : (r / 12.92);
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / (1.0f + 0.055), 2.4) : (g / 12.92);
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / (1.0f + 0.055), 2.4) : (b / 12.92);

            double X = r * 0.649926 + g * 0.103455 + b * 0.197109;
            double Y = r * 0.234327 + g * 0.743075 + b * 0.022598;
            double Z = r * 0.0000000 + g * 0.053077 + b * 1.035763;

            double x = X / (X + Y + Z);
            double y = Y / (X + Y + Z);
            xy[0] = x;
            xy[1] = y;
            return xy;
        }

        private int hueFromRGB(ColorDTO color)
        {
            int red = color.r;
            int green = color.g;
            int blue = color.b;

            float min = Math.Min(Math.Min(red, green), blue);
            float max = Math.Max(Math.Max(red, green), blue);

            if (min == max)
            {
                return 0;
            }

            float hue = 0f;
            if (max == red)
            {
                hue = (green - blue) / (max - min);

            }
            else if (max == green)
            {
                hue = 2f + (blue - red) / (max - min);

            }
            else
            {
                hue = 4f + (red - green) / (max - min);
            }

            hue = hue * 60;
            if (hue < 0) hue = hue + 360;

            return (int)Math.Round(hue);
        }
    }
}
