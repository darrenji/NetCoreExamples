using System;

namespace TotalNetCore.ByMrXiao.Autofac增强容器能力.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
