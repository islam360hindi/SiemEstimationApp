using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EstimationApp.Util;

namespace EstimationApp.Util
{
    public class PrintToFilePrinter : IPrinter
    {
        public async Task Print(string text)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            File.WriteAllText(folderPath, text, encoding: Encoding.UTF8);
            await Task.Delay(100); //mocking progress
        }
    }
}
