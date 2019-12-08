using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Services
{
    public class XlsxReportService : ReportServiceBase
    {
        protected override DataRow[] GetDataRows(string text)
        {
            return new DataRow[0];
        }

        protected override string RecognizableExtension => "xlsx";
    }
}