using System.IO;
using System.Linq;
using Xrm.ReportUtility.Infrastructure;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Services
{
    // Handler в цепочке обязанностей
    public abstract class ReportServiceBase : IReportService
    {
        public Report CreateReport(ReportConfig config, string fileName)
        {
            var dataTransformer = DataTransformerCreator.CreateTransformer(config);

            var text = File.ReadAllText(fileName);
            var data = GetDataRows(text);
            return dataTransformer.TransformData(data);
        }

        public bool Validate(string fileName)
        {
            return fileName.EndsWith($".{RecognizableExtension}");
        }

        protected abstract DataRow[] GetDataRows(string text);
        protected abstract string RecognizableExtension { get; }
    }
}
