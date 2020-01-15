using System;
using System.Collections.Generic;

namespace TotalNetCore.AdapterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //var xmlConverter = new XmlConverter();
            //xmlConverter.GetXML();

            //var lst = new List<Manufacturer> {
            //    new Manufacturer{City="QingDao",Name="Darren",Year=2019},
            //    new Manufacturer{City="NanTong",Name="Darren",Year=2020}
            //};
            //var jsonConverter = new JsonConverter(lst);
            //jsonConverter.ConvertToJson();


            var xmlConverter = new XmlConverter();
            var adapter = new XmlToJsonAdapter(xmlConverter);
            adapter.ConvertXmlToJson();
        }
    }
}
