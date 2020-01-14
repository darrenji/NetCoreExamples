using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    public class ProductStockReportChainedBuilder : IProductStockReportChainedBuilder
    {
        private IEnumerable<Product> _products;
        private ProductStockReport _productStockReport;

        public ProductStockReportChainedBuilder(IEnumerable<Product> products)
        {
            _products = products;
            _productStockReport = new ProductStockReport();
        }
        public IProductStockReportChainedBuilder BuildBody()
        {
            _productStockReport.BodyPart = string.Join(Environment.NewLine, _products.Select(t=> $"Product name:{t.Name}, Product price:{t.Price}"));
            return this;
        }

        public IProductStockReportChainedBuilder BuildFooter()
        {
            _productStockReport.FooterPart = $"{Environment.NewLine}Footer部分";
            return this;
        }

        public IProductStockReportChainedBuilder BuildHeader()
        {
            _productStockReport.HeaderPart = $"Header部分\n";
            return this;
        }

        public ProductStockReport GetReport()
        {
            var productStockReport = _productStockReport;
            Clear();
            return productStockReport;
        }

        private void Clear() => _productStockReport = new ProductStockReport();
    }
}
