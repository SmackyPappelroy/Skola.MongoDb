// See https://aka.ms/new-console-template for more information

using Mongo.Common;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://localhost:7118/api/ShopItems");
request.Method = "GET";
//specify other request properties

try
{
    var response = (HttpWebResponse)request.GetResponse();
    using (var streamReader = new StreamReader(response.GetResponseStream()))
    {
        var json = streamReader.ReadToEnd();
        List<ShopItem> shop = JsonSerializer.Deserialize<List<ShopItem>>(json);
    }
}
catch (Exception ex) { }