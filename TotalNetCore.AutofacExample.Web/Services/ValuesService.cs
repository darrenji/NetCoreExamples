using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.Services
{
    public class ValuesService : IValuesService
    {
        private readonly ILogger<ValuesService> _logger;

        public ValuesService(ILogger<ValuesService> logger)
        {
            _logger = logger;
        }
        public string Find(int id)
        {
            _logger.LogDebug("{method} called with {id}", nameof(Find), id);
            return $"value{id}";
        }

        public IEnumerable<string> FindAll()
        {
            _logger.LogDebug("{method} called", nameof(FindAll));
            return new[] { "value1","value2"};
        }
    }
}
