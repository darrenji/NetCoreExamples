using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    /// <summary>
    /// 引用建造者的类
    /// </summary>
    public class ProductStockReportDirector
    {
        private readonly IProductStockReportBuilder _productStockReportBuilder;

        public ProductStockReportDirector(IProductStockReportBuilder productStockReportBuilder)
        {
            _productStockReportBuilder = productStockReportBuilder;
        }

        public void BuildStockReport()
        {
            _productStockReportBuilder.BuildHeader();
            _productStockReportBuilder.BuildBody();
            _productStockReportBuilder.BuildFooter();
        }
    }
}
