using System;
namespace EstimationApp.Services
{
    public interface ILogService
    {
        void WriteError(Exception ex);
        void Write(string log);
    }
}
