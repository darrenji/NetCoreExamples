using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    public class ProductStockReportChainedDirector
    {
        private readonly IProductStockReportChainedBuilder _builder;

        public ProductStockReportChainedDirector(IProductStockReportChainedBuilder builder)
        {
            _builder = builder;
        }

        public void BuildStockReport()
        {
            _builder
                .BuildHeader()
                .BuildBody()
                .BuildFooter();
        }
    }
}
