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
            Xls = new XlsFile(1, TExcelFileFormat.v2016, true);

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
            Xls.Save(FileStream, TFileFormats.Xlsx);

            FileStream.Close();
        }
        public void Execute(IEnumerable<Model> models)
        {
            var type = typeof(Model);
            IEnumerable<object> fields = default;
            var i = 1;
            var j = 1;
            
            foreach (var model in models)
            {
                fields = type.GetFields().Select(l => l.GetValue(model));
                foreach (var item in fields)
                {
                    Xls.SetCellValue(i, j++, item);
                }
                i++;
            }
            // Save to file
            Xls.Save(FileStream, TFileFormats.Xlsx);
            FileStream.Close();
        }
    }
}
