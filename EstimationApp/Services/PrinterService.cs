using System;
using EstimationApp.Util;

namespace EstimationApp.Services
{
    public class PrinterService : IPrinterService
    {
        public void Print(string text, PrinterType printerType = PrinterType.PrintToScreen)
        {
            IPrinter printer = new PrinterBuilder()
                .SetPrinterType(printerType)
                .Build();
            printer.Print(text);
        }
    }
}
