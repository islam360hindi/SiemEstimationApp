using System;
using System.Diagnostics;

namespace EstimationApp.Services
{
    public class LogService : ILogService
    {
        public void Write(string log)
        {
            //use custom analytics service such as app center when configured
            Debug.WriteLine(log);
        }

        public void WriteError(Exception ex)
        {
            //use custom analytics service such as app center when configured
            Debug.WriteLine(ex);
        }
    }
}
