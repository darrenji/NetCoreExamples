using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Marketplace.Api;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Marketplace.Controllers
{
    [Route("api/ad")]
    public class ClassifiedAdsCommandApiController : Controller
    {
        //private readonly IHandleCommand<Contracts.ClassifiedAds.V1.Create> _createAdCommandHandler;
        private readonly Func<IHandleCommand<Contracts.ClassifiedAds.V1.Create>> _createFactory;//这种写法在注册时候可能需要用到AutoFac。这样说来，使用注入工厂，可以减少代码量，否则在构造函数中会有很多依赖或者说容器服务

        public ClassifiedAdsCommandApiController(/*IHandleCommand<Contracts.ClassifiedAds.V1.Create> createAdCommandHandler*/Func<IHandleCommand<Contracts.ClassifiedAds.V1.Create>> createFactory)
        {
            //_createAdCommandHandler = createAdCommandHandler;
            _createFactory = createFactory;
        }
        [HttpPost]
       public async Task<IActionResult> Post(Contracts.ClassifiedAds.V1.Create request)
        {
            //await  _createAdCommandHandler.Handle(request);
            await _createFactory().Handle(request);
            return Ok();
        }
    }
}
