namespace Xrm.ReportUtility.Models
{
    // Можно реализовать билдер для создания конфига.
    // Это позволить валидировать обязательность полей (2 хотелка)
    public class ReportConfig
    {
        public bool WithData { get; set; }

        public bool WithIndex { get; set; }
        public bool WithTotalVolume { get; set; }
        public bool WithTotalWeight { get; set; }

        public bool VolumeSum { get; set; }
        public bool WeightSum { get; set; }
        public bool CostSum { get; set; }
        public bool CountSum { get; set; }
    }
}
