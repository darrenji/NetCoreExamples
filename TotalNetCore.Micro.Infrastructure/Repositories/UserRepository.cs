using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.Domain.UserAggregate;
using TotalNetCore.Micro.InfrastructureCore;

namespace TotalNetCore.Micro.Infrastructure.Repositories
{
   public  class UserRepository : Repository<User, long, DomainContext>
    {

        public UserRepository(DomainContext context) : base(context)
        {

        }
    }
}
