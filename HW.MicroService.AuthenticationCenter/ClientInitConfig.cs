using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.MicroService.AuthenticationCenter
{
    /// <summary>
    /// 自定义配置文件
    /// </summary>
    public class ClientInitConfig
    {
        /// <summary>
        /// 获取资源的使用范围
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            { 
                new ApiScope(name:"User.Get",displayName:"获取用户数据"),

                new ApiScope(name:"Health.Check",displayName:"健康检查")

            };
        }
        /// <summary>
        /// 权限能使用的资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("UserApi","Invoice API")
                {
                    Scopes = {"User.Get","Health.Check"}
                }

            };
        }
        /// <summary>
        /// 客户端认证条件
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                     ClientId="HW.MicroService.AuthenticationDemo",
                     ClientSecrets={new Secret("HW123456".Sha256()) },
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     AllowedScopes=new[]{ "User.Get", "Health.Check" },
                     Claims=new List<ClientClaim>(){
                         new ClientClaim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                         new ClientClaim(IdentityModel.JwtClaimTypes.NickName,"HW"),
                         new ClientClaim("eMail","wanghui.123.love@sina.com")
                        }
                }
            };
        }
    }
}
