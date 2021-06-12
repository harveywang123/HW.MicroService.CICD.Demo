using Consul;
using HW.MicroService.Client.Models;
using HW.MicroService.Interfaces;
using HW.MicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HW.MicroService.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private static int index;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {

            #region 1.单体架构
            //this.ViewBag.Users = _userService.UserAll(); 
            #endregion


            #region 2.分布式架构

            #region 2.1直接访问服务实例

            //string url = string.Empty;
            //url = "http://localhost:5726/api/users/all";
            //url = "http://localhost:5727/api/users/all";
            //url = "http://localhost:5728/api/users/all";

            #endregion

            #region 通过 Ngnix网关访问服务实例，默认轮训。
            //string url = "http://localhost:8080/api/users/all";

            #endregion

            #region 通过 Consul 服务发现来执行服务实例。
            //发现服务
            //string url = "http://HWService/api/users/all";

            //ConsulClient client = new ConsulClient(config =>
            //{
            //    config.Address = new Uri("http://localhost:8500/");
            //    config.Datacenter = "dc1";
            //});

            //var response = client.Agent.Services().Result.Response;
            //foreach (var item in response)
            //{
            //    Console.WriteLine("***************************");
            //    Console.WriteLine(item.Key);
            //    var service = item.Value;
            //    Console.WriteLine($"{service.Address}--{service.Port}--{service.Service}");
            //    Console.WriteLine("***************************");
            //}
            //var uri = new Uri(url);
            //string groupName = uri.Host;
            //AgentService agentService = null;

            //var serviceDirectory = response.Where(s => s.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase)).ToArray();

            ////1、写死
            //{
            //    // agentService = serviceDirectory[0].Value;
            //}

            ////2、轮训
            //{
            //    if (index >= 1000)
            //    {
            //        index = 0;
            //    }

            //    agentService = serviceDirectory[index++ % serviceDirectory.Length].Value;
            //}

            //{
            //    ////3、随机
            //    //var indexResult = new Random(index++).Next(0, serviceDirectory.Length);
            //    //agentService = serviceDirectory[indexResult].Value;
            //}

            //url = $"{uri.Scheme}://{agentService.Address}:{agentService.Port}{uri.PathAndQuery}";

            #endregion

            #region 通过ocelot 网关访问服务实例。

            //string url = "http://localhost:6299/gate/users/all";//这个地址是网关的


            #endregion

            #region Ocelot集群的Nginx反向代理

            //string url = "http://localhost:8080/gate/users/all";//Ocelot 集群的Nginx反向代理地址
            #endregion

            #region identityserver4

           // string url = "http://localhost:6400/gate/users/all?Authentication=bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjFCRDc2NkU4QTBBRkRDRERCMEZENjc4RDM4OEIxQzQxIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MjM0MDA0MTEsImV4cCI6MTYyMzQwNDAxMSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo3Mjk5IiwiYXVkIjoiVXNlckFwaSIsImNsaWVudF9pZCI6IkhXLk1pY3JvU2VydmljZS5BdXRoZW50aWNhdGlvbkRlbW8iLCJjbGllbnRfcm9sZSI6IkFkbWluIiwiY2xpZW50X25pY2tuYW1lIjoiSFciLCJjbGllbnRfZU1haWwiOiJ3YW5naHVpLjEyMy5sb3ZlQHNpbmEuY29tIiwianRpIjoiMDE2NDEzMjlGQkFBMjE0MDI3MUIyQ0VBNkY0RjMxNUIiLCJpYXQiOjE2MjM0MDA0MTEsInNjb3BlIjpbIlVzZXIuR2V0Il19.TekNiKThAIj-qsCYZ0ZHFCigzEfP0IKHJmTngHHQ2nq3dIAZ2MXuYHisJb2G_BWfHXwdsjWYsoD50zmS4Ys0DXcGtYRvXzuMCsCQG8zetjqLiDV2dEisyTvNOOS84n7_ubo-xd1mwUUlbH9kKDF-PzpKT8hftZlvSgH2HGthh7cc3qaO7k8XLWRGV-hCdfOkTsO3S6WCOVab0vjxr0VK9INqp7siiVY2odlps8SrpI91CkR0-gJqhbUuP9JorFduOUPsbjIFQ8t3clFM2XYXlgQ2NyuGe7f1gCCVIM7VkBuo96EpstjxDfnKKaLzcr1RfIttDTpTQCuF32Q8CKGuFw";//Ocelot 集群的Nginx反向代理地址
            #endregion


            #region linux Ocelot集群访问，追加访问Token,

             string url = "http://192.168.1.103:8083/gate/users/all?Authentication=bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjFCRDc2NkU4QTBBRkRDRERCMEZENjc4RDM4OEIxQzQxIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MjM0MDA0MTEsImV4cCI6MTYyMzQwNDAxMSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo3Mjk5IiwiYXVkIjoiVXNlckFwaSIsImNsaWVudF9pZCI6IkhXLk1pY3JvU2VydmljZS5BdXRoZW50aWNhdGlvbkRlbW8iLCJjbGllbnRfcm9sZSI6IkFkbWluIiwiY2xpZW50X25pY2tuYW1lIjoiSFciLCJjbGllbnRfZU1haWwiOiJ3YW5naHVpLjEyMy5sb3ZlQHNpbmEuY29tIiwianRpIjoiMDE2NDEzMjlGQkFBMjE0MDI3MUIyQ0VBNkY0RjMxNUIiLCJpYXQiOjE2MjM0MDA0MTEsInNjb3BlIjpbIlVzZXIuR2V0Il19.TekNiKThAIj-qsCYZ0ZHFCigzEfP0IKHJmTngHHQ2nq3dIAZ2MXuYHisJb2G_BWfHXwdsjWYsoD50zmS4Ys0DXcGtYRvXzuMCsCQG8zetjqLiDV2dEisyTvNOOS84n7_ubo-xd1mwUUlbH9kKDF-PzpKT8hftZlvSgH2HGthh7cc3qaO7k8XLWRGV-hCdfOkTsO3S6WCOVab0vjxr0VK9INqp7siiVY2odlps8SrpI91CkR0-gJqhbUuP9JorFduOUPsbjIFQ8t3clFM2XYXlgQ2NyuGe7f1gCCVIM7VkBuo96EpstjxDfnKKaLzcr1RfIttDTpTQCuF32Q8CKGuFw";//Ocelot 集群的Nginx反向代理地址
            #endregion

            string content = InvokeAPI(url);
            this.ViewBag.Users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<User>>(content);

            Console.WriteLine($"This is {url} Invoke.");
            #endregion
            return View();
        }

        private static string InvokeAPI(string url)
        {
            using(HttpClient client = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = client.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
