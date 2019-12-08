using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Interfaces
{
    public interface IReportService
    {
        Report CreateReport(ReportConfig config, string fileName);
        bool Validate(string fileName);
    }
}
