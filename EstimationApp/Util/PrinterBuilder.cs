
namespace EstimationApp.Util
{
    public class PrinterBuilder
    {
        private PrinterType _printerType = PrinterType.PrintToScreen;

        public PrinterBuilder SetPrinterType(PrinterType printerType)
        {
            _printerType = printerType;
            return this;
        }

        public IPrinter Build()
        {
            switch (_printerType)
            {
                case PrinterType.PrintToFile:
                    return new PrintToFilePrinter();
                case PrinterType.PrintToPaper:
                    return new PrintToPaperPrinter();
                default:
                    return new PrintToScreenPrinter();
            }
        }
    }

    public enum PrinterType
    {
        PrintToScreen,
        PrintToPaper,
        PrintToFile
    }
}
