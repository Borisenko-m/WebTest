using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using WebTest.Models;
namespace WebTest.Classes.ExcelIO
{
    public class ExcelIO<Model>
    {
        /// <summary>
        /// Class that convert json to xls
        /// </summary>
        /// <param name="name">Name of file.</param>
        public ExcelIO(string name, string content)
        {

            var path = @$"..\{name}.xlsx";
            FileStream = File.Exists(path) ?
                new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite) :
                new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            Path = FileStream.Name;
        }


        public string Path { get; set; }
        FileStream FileStream { get; set; }
        public void Test()
        {
            using var package = new ExcelPackage(FileStream);
            var sheet = package.Workbook.Worksheets.Add("My Sheet");
            
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    sheet.SetValue(i, j, i + " " + j);
                }
            }

            package.Save();
            FileStream.Close();
        }
        public void Execute(IEnumerable<Model> models)
        {
            var type = typeof(Model);
            IEnumerable<object> fields = default;
            var i = 1;
            var j = 1;
            using var package = new ExcelPackage(FileStream);
            var sheet = package.Workbook.Worksheets.Add("My Sheet");
            foreach (var model in models)
            {
                fields = type.GetFields().Select(l => l.GetValue(model));
                foreach (var item in fields)
                {
                    sheet.SetValue(i, j++, item);
                }
                i++;
            }
            // Save to file
            package.Save();
            FileStream.Close();
        }
    }
}
