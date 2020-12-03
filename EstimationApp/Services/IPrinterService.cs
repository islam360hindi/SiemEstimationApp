using System;
using EstimationApp.Util;

namespace EstimationApp.Services
{
    public interface IPrinterService
    {
        void Print(string text, PrinterType printerType = PrinterType.PrintToScreen);
    }
}
