using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    /// <summary>
    /// 建造者实现
    /// </summary>
    public class ProductStockReportBuilder : IProductStockReportBuilder
    {
        private IEnumerable<Product> _products;
        private ProductStockReport _productStockReport;

        public ProductStockReportBuilder(IEnumerable<Product> products)
        {
            _products = products;
            _productStockReport = new ProductStockReport();
        }
        public void BuildBody()
        {
            _productStockReport.BodyPart = string.Join(Environment.NewLine, _products.Select(t=> $"Product name:{t.Name}, Product price:{t.Price}"));
        }

        public void BuildFooter()
        {
            _productStockReport.FooterPart = $"\n这里是Footer部分";
        }

        public void BuildHeader()
        {
            _productStockReport.HeaderPart = $"Header部分，发生时间：{DateTime.Now}\n";
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
