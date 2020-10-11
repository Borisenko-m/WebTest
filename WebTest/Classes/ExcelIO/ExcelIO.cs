using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using System.Web;
namespace WebTest.Classes.ExcelIO
{
    public class ExcelIO
    {
        public ExcelIO(string name) { Name = name; }

        public string Name { get; set; }
        public string Path { get; set; }

        public string Test(string name)
        {
            var cat = @"C:\Users\davil\source\repos\WebTest";
            Path = cat + "buffer" + ".xlsx";
            var fs = new FileStream(Path, FileMode.Create ,FileAccess.ReadWrite);
            using var package = new ExcelPackage(fs);

            var sheet = package.Workbook.Worksheets.Add("My Sheet");
            sheet.Cells["A1"].Value = $"Hello {name}!";
            sheet.Cells["A2"].Value = "Hello fcking World!";

            var chart = sheet.Drawings.AddChart("", eChartType.ColumnStacked);
            chart.SetPosition(3, 0, 0, 0);
            chart.Description = "3e432432432";
            chart.Name = "2329199191911kkk1k1kk1";
            

            // Save to file
            package.Save();
            fs.Close();
            return Path;
        }

        ~ExcelIO()
        {
            File.Delete(Path);
        }
    }
}
