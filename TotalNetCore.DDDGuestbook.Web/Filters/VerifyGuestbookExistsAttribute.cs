﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.Interfaces;

namespace TotalNetCore.DDDGuestbook.Web.Filters
{
    public class VerifyGuestbookExistsAttribute : TypeFilterAttribute
    {
        public VerifyGuestbookExistsAttribute() : base(typeof(VerifyGuestbookExistsFilter))
        {

        }

        private class VerifyGuestbookExistsFilter : IAsyncActionFilter
        {
            private readonly IRepository _repository;

            public VerifyGuestbookExistsFilter(IRepository repository)
            {
                _repository = repository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    if (context.ActionArguments["id"] is int id)
                    {
                        if (_repository.GetById<Guestbook>(id) is null)
                        {
                            context.Result = new NotFoundObjectResult(id);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
