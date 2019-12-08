using System;
using System.IO;
using System.Linq;
using Xrm.ReportUtility.Infrastructure;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;
using Xrm.ReportUtility.Services;

namespace Xrm.ReportUtility
{
    /// <summary>
    ///     Реализация интерфейса-фасада
    /// </summary>
    public class ReportServiceFacade : IReportFacade
    {
        public ReportServiceFacade(string[] args, IReportService[] reportServices)
        {
            this.args = args;
            this.reportServices = reportServices;
        }

        public Report CreateReport()
        {
            var service = CreateReportService();
            var config = ParseConfig(args);
            return service.CreateReport(config, fileName);
        }

        public IReportService CreateReportService()
        {
            foreach (var service in reportServices)
            {
                // Паттерн цепочка обязанностей.
                // Каждый сервис умеет понимать, сможет ли он распарсить файл или нет.
                // С его помощью набор сервисов можно задавать динамически.
                if (service.Validate(fileName))
                    return service;
            }
            
            throw new NotSupportedException($"Wrong file '{fileName}': this extension not supported");
        }

        public ReportConfig ParseConfig(string[] args)
        {
            return new ReportConfig
            {
                WithData = args.Contains("-data"),

                WithIndex = args.Contains("-withIndex"),
                WithTotalVolume = args.Contains("-withTotalVolume"),
                WithTotalWeight = args.Contains("-withTotalWeight"),

                VolumeSum = args.Contains("-volumeSum"),
                WeightSum = args.Contains("-weightSum"),
                CostSum = args.Contains("-costSum"),
                CountSum = args.Contains("-countSum")
            };
        }

        private string fileName => args[0];

        private readonly string[] args;
        private readonly IReportService[] reportServices;
    }
}