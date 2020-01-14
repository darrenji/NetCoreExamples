using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    /// <summary>
    /// 建造者接口
    /// </summary>
    public interface IProductStockReportBuilder
    {
        void BuildHeader();
        void BuildBody();
        void BuildFooter();
        ProductStockReport GetReport();
    }
}
