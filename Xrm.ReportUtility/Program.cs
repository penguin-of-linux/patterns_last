using System;
using System.Linq;
using System.Reflection;
using GroboContainer.Core;
using GroboContainer.Impl;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility
{
    public static class Program
    {
        // "Files/table.txt" -data -weightSum -costSum -withIndex -withTotalVolume
        public static void Main(string[] args)
        {
            var container = InitializeContainer(args);
            var facade = container.Get<IReportFacade>();
            var report = facade.CreateReport();

            PrintReport(report);

            Console.WriteLine("");
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }

        // Паттерн dependency injection
        // Позволяет указать конкреные реализации в 1 точке программы. В дальнейшем можно использовать только абстракции.
        // Также позволяет добавлять новые реализации (IReportService) без написания дополнительного кода
        //    (нужно просто реализовать IReportService).
        private static IContainer InitializeContainer(string[] args)
        {
            var container = new Container(new ContainerConfiguration(Assembly.GetExecutingAssembly()));
            var reportServices = container.GetAll(typeof(IReportService)).Cast<IReportService>().ToArray();
            container.Configurator.ForAbstraction<IReportFacade>().UseInstances(new ReportServiceFacade(args, reportServices));
            return container;
        }
        
        private static void PrintReport(Report report)
        {
            if (report.Config.WithData && report.Data != null && report.Data.Any())
            {
                var headerRow = "Наименование\tОбъём упаковки\tМасса упаковки\tСтоимость\tКоличество";
                var rowTemplate = "{1,12}\t{2,14}\t{3,14}\t{4,9}\t{5,10}";

                if (report.Config.WithIndex)
                {
                    headerRow = "№\t" + headerRow;
                    rowTemplate = "{0}\t" + rowTemplate;
                }
                if (report.Config.WithTotalVolume)
                {
                    headerRow = headerRow + "\tСуммарный объём";
                    rowTemplate = rowTemplate + "\t{6,15}";
                }
                if (report.Config.WithTotalWeight)
                {
                    headerRow = headerRow + "\tСуммарный вес";
                    rowTemplate = rowTemplate + "\t{7,13}";
                }

                Console.WriteLine(headerRow);

                for (var i = 0; i < report.Data.Length; i++)
                {
                    var dataRow = report.Data[i];
                    Console.WriteLine(rowTemplate, i + 1, dataRow.Name, dataRow.Volume, dataRow.Weight, dataRow.Cost, dataRow.Count, dataRow.Volume * dataRow.Count, dataRow.Weight * dataRow.Count);
                }

                Console.WriteLine();
            }

            if (report.Rows != null && report.Rows.Any())
            {
                Console.WriteLine("Итого:");
                foreach (var reportRow in report.Rows)
                {
                    Console.WriteLine(string.Format("  {0,-20}\t{1}", reportRow.Name, reportRow.Value));
                }
            }
        }
    }
}