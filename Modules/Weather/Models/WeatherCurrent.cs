namespace HomeAppLBO.Modules.Weather.Models
{
    public sealed class WeatherCurrent
    {
        public float TemperatureCelsius { get; set; }
        public string Condition { get; set; }
        public float HumidityPercent { get; set; }
        public float WindSpeedKmh { get; set; }
        public float PressureHpa { get; set; }
        public float UvIndex { get; set; }
        

        public WeatherCurrent()
        {
            TemperatureCelsius = 0;
            Condition = string.Empty;
            HumidityPercent = 0;
            WindSpeedKmh = 0;
            PressureHpa = 0;
            UvIndex = 0;
        }
    }
}