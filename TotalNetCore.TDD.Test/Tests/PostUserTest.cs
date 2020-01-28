using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.TDD.Infra.Models;
using Xunit;

namespace TotalNetCore.TDD.Test.Tests
{
    public class PostUserTest : BaseTest
    {
        [Fact]
        public void Fact_PostUser()
        {
            var user = new User(0, "h", 25, true);
            ctx.User.Add(user);
            ctx.SaveChanges();

            Assert.Equal(1, user.Id);
        }
    }
}
