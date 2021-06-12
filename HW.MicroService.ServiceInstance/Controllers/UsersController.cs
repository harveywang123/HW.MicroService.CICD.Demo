using HW.MicroService.Interfaces;
using HW.MicroService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HW.MicroService.ServiceInstance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private IConfiguration _configuration;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public User Get(int id)
        {
            return this._userService.FindUser(id);
        }
        /// <summary>
        /// 获取所有记录。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public IEnumerable<User> Get()
        {
            Console.WriteLine($"This is UsersController {this._configuration["port"]} Invoke");

            return this._userService.UserAll().Select(user => new User
            {
                ID = user.ID,
                Name = user.Name,
                Account = user.Account,
                Password = user.Password,
                Email = user.Email,
                Role = $"{this._configuration["ip"]}：{this._configuration["port"]}",
                LoginTime = user.LoginTime
            }) ;
        }
        /// <summary>
        /// 超时处理。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TimeOut")]
        public IEnumerable<User> TimeOut()
        {
            Console.WriteLine($"This is Timeout Start");
            Thread.Sleep(3000);
            Console.WriteLine($"This is timeout end");
            return this._userService.UserAll().Select(user => new User
            {
                ID = user.ID,
                Name = user.Name,
                Account = user.Account,
                Password = user.Password,
                Email = user.Email,
                Role = $"{this._configuration["ip"]}：{this._configuration["port"]}",
                LoginTime = user.LoginTime
            });
        }

    }
}
