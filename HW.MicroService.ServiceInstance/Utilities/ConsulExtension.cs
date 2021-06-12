using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.MicroService.ServiceInstance.Utilities
{
    /// <summary>
    /// consul 静态扩展类
    /// </summary>
    public static class ConsulExtension
    {
        public static Uri  Uri { get; set; }
        public static string DataCenter { get; set; }
        static ConsulExtension()
        {
            Uri = new Uri("http://localhost:8500");
            DataCenter = "dc1";
        }
        /// <summary>
        /// 向 Consul 服务中心注册服务实例。
        /// </summary>
        /// <param name="configuration">配置对象</param>
        /// <param name="consulServiceName">在 Consul 服务中心注册的服务类别名称，多个实例的 ID 可以属于一个服务类别名称。</param>
        /// <param name="serviceID">服务实例的主键值，必须唯一。</param>
        public static void ConsulRegist(this IConfiguration configuration, string consulServiceName, string serviceID)
        {
            if(string.IsNullOrWhiteSpace(consulServiceName))
            {
                throw new ArgumentNullException("consulServiceName is null");
            }
            if (string.IsNullOrWhiteSpace(serviceID))
            {
                throw new ArgumentNullException("serviceID is null");
            }
            string consulAddress = configuration["ConsulAddress"];
            string consulCenter = configuration["ConsulCenter"];

            if (!string.IsNullOrWhiteSpace(consulAddress))
            {
                Uri = new Uri(consulAddress);
            }
            if (!string.IsNullOrWhiteSpace(consulCenter))
            {
                DataCenter = consulCenter;
            }
            using (ConsulClient client = new ConsulClient(config => {
                config.Address = Uri;
                config.Datacenter = DataCenter;
            }))
            {
                string ip = configuration["ip"];
                int port = int.Parse(configuration["port"]);
                int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = serviceID,
                    Name = consulServiceName,
                    Address = ip,
                    Port = port,
                    Tags = new string[] { weight.ToString() },
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(12),
                        HTTP = $"http://{ip}:{port}/API/Health/Index",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }
                }).Wait() ;
                Console.WriteLine($"注册服务：{ip}:{port}--Weight:{weight}");
            }
        }
   
        public static void ConsulDown(this IHostApplicationLifetime applicationLifetime, string serviceID)
        {
            if (string.IsNullOrWhiteSpace(serviceID))
            {
                throw new ArgumentNullException("serviceID is null");
            }

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                using (var consulClient = new ConsulClient(config => { config.Address = Uri; config.Datacenter = DataCenter; }))
                {
                    Console.WriteLine("服务已经退出");
                    consulClient.Agent.ServiceDeregister(serviceID);
                }
             });
        }
    
    }
}
