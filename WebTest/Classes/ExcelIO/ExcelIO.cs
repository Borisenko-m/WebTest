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

        public void Execute(IEnumerable<Model> models)
        {
            var type = typeof(Model);
            IEnumerable<object> fields = default; 
            var i = 0;
            var j = 0;
            using var package = new ExcelPackage(FileStream);
            var sheet = package.Workbook.Worksheets.Add("My Sheet");
            foreach (var model in models)
            {
                fields = type.GetFields().Select(l => l.GetValue(model));
                foreach (var item in fields)
                {
                    sheet.Cells[i,j++].Value = item;
                }
                i++;
            }
            // Save to file
            package.Save();
            FileStream.Close();
        }
    }
}
