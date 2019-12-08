using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility
{
    /// <summary>
    ///     Предоставляет интерфейс-фасад для получения отчетов
    /// </summary>
    // Чтобы создать отчет, нужно распарсить аргументы, выбрать нужный IReportService, затем создать отчет.
    // Для конечного пользователя это слишком сложно, нужно предоставить упрощенный интерфейс
    public interface IReportFacade
    {
        Report CreateReport();
    }
}