using System;

namespace TotalNetCore.ByMrXiao.依赖倒置作用域.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
