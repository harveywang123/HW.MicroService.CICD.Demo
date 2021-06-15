using HW.MicroService.Interfaces;
using HW.MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.MicroService.Services
{
    /// <summary>
    /// 实现用户服务接口的实现类型。
    /// </summary>
    public class UserService : IUserService
    {
        private IList<User> dataList;

        /// <summary>
        /// 初始化类型的实例
        /// </summary>
        public UserService()
        {
            dataList = new List<User>()
            { new User {ID=1,Name="黄飞鸿4",Account="HuangFeiHong2",Password="HuangFeiHong123456",Email="huangFeiHong@sina.com", Role="Admin", LoginTime=DateTime.Now },
            new User {ID=2,Name="洪熙官4",Account="HongXiGuan2",Password="HongXiGuan54667",Email="HongXiGuan@sina.com", Role="Admin", LoginTime=DateTime.Now.AddDays(-5) },
            new User {ID=3,Name="方世玉4",Account="FangShiYu2",Password="FangShiYu112233",Email="fangShiYu@163.com", Role="Admin", LoginTime=DateTime.Now.AddDays(-30) },
            new User {ID=4,Name="苗翠花4",Account="MiaoCuiHua2",Password="MiaoCuiHua887766",Email="miaoCuiHua@sohu.com", Role="Admin", LoginTime=DateTime.Now.AddDays(-90) },
            new User {ID=5,Name="严咏春4",Account="YanYongChun2",Password="YanYongChun09392",Email="yanYongChun@263.com", Role="Admin", LoginTime=DateTime.Now.AddMinutes(-50) }};
        }

        /// <summary>
        /// 查找指定主键的用户实例对象。
        /// </summary>
        /// <param name="id">用户的主键。</param>
        /// <returns>返回查找到的用户实例对象。</returns>
        public User FindUser(int id)
        {
            return dataList.FirstOrDefault(user => user.ID == id);
        }

        /// <summary>
        /// 获取所有用户的实例集合。
        /// </summary>
        /// <returns>返回所有的用户实例。</returns>
        public IEnumerable<User> UserAll()
        {
            return dataList;
        }
    }
}
