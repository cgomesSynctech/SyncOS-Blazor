using Modelos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  class ConsumirWS
    {

        public  HttpStatusCode RequestPOSTJSON(string url, string jsonObj)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(url);
            var request = new RestRequest("/Acesso", RestSharp.Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> WSInsertUpdate(string jsonObj)
        {
            var client = new RestClient("http://191.253.80.67:5555");
            var request = new RestRequest("/Cliente", RestSharp.Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            return response.StatusCode;
        }
    }
}

