using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.BuilderPattern
{
    /// <summary>
    /// 这个类有一些属性需要设置
    /// </summary>
    public class ProductStockReport
    {
        public string HeaderPart { get; set; }
        public string BodyPart { get; set; }
        public string FooterPart { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine(HeaderPart)
                .AppendLine(BodyPart)
                .AppendLine(FooterPart)
                .ToString();
        }
    }
}
