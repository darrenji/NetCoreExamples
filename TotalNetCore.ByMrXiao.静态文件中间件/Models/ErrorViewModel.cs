using System;

namespace TotalNetCore.ByMrXiao.静态文件中间件.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
