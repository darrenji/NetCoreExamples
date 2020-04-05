using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.Micro.API.Application.Queries
{
    public class MyOrderQuery : IRequest<List<string>>
    {
        public string UserName { get; private set; }
        public MyOrderQuery(string userName) 
        {
            UserName = userName;
        }
    }
}
