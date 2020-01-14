using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    public interface IProductStockReportChainedBuilder
    {
        IProductStockReportChainedBuilder BuildHeader();
        IProductStockReportChainedBuilder BuildBody();
        IProductStockReportChainedBuilder BuildFooter();
        ProductStockReport GetReport();
    }
}
