using System;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
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
            Xls = new XlsFile(1, TExcelFileFormat.v2010, true);

            //for linux
            //var path = @$"../share/{name}.xlsx";
            var path = @$"..\{name}.xlsx";

            FileStream = File.Exists(path) ?
                new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite) :
                new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            Path = FileStream.Name;
        }

        XlsFile Xls;
        public string Path { get; set; }
        FileStream FileStream { get; set; }
        public void Test()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Xls.SetCellValue(i, j, i + " " + j);
                }
            }
            Xls.Save(FileStream);

            FileStream.Close();
        }
        public FileStream Execute(IEnumerable<Model> models)
        {
            var type = typeof(Model);
            IEnumerable<object> fields = default;
            var i = 1;
            var j = 1;
            
            foreach (var model in models)
            {
                fields = type.GetProperties().Select(l => l.GetValue(model));
                j = 1;
                foreach (var item in fields)
                {
                    Xls.SetCellValue(i, j, item);
                    j++;
                }
                i++;
            }
            // Save to file
            Xls.Save(FileStream);
            return FileStream;
        }
    }
}
