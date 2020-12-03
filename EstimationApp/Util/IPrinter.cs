using System;
using System.Threading.Tasks;

namespace EstimationApp.Util
{
    public interface IPrinter
    {
        Task Print(string text);
    }
}
