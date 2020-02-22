using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.RestEaseExample.Models;

namespace TotalNetCore.RestEaseExample.Interfaces
{
    [Header("User-Agent","RestEase")] //Github要求
   public  interface IGitHupApi
    {
        [Get("users/{userId}")]
        Task<User> GetUserAsync([Path]string userId);//调用远程接口的方法名由自己定义
    }
}
