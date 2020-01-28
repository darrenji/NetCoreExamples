using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.TDD.Infra.Repositories;

namespace TotalNetCore.TDD.Test.Tests
{
    public class BaseTest
    {
        protected MyContext ctx;

        public BaseTest(MyContext ctx = null)
        {
            this.ctx = ctx ?? GetInMemoryDBContext();
        }

        protected MyContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()//在容器中加入有关EntityFramework的内存数据库
                .BuildServiceProvider();//返回容器的Provider

            var builder = new DbContextOptionsBuilder<MyContext>();//DbContextOptions的所有配置通过DbContectOptionsBuilder<DbContext>
            var options = builder
                .UseInMemoryDatabase("test")//数据库名称
                .UseInternalServiceProvider(serviceProvider)//用哪个ServiceProvider,即在哪个依赖倒置容器中
                .Options;

            MyContext dbContext = new MyContext(options);//把DbContextOptions交给DbContext
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
