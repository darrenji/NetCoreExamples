using System;

namespace TotalNetCore.ByMrXiao.依赖注入例子.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
