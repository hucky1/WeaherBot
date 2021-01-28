using System;
using System.Threading.Tasks;

namespace TelegramBotModels
{
    /// <summary>
    /// Keep information about weather indicators
    /// </summary>
    public class TemperatureInfoModel
    {
        public float Temp { get; set; }
        public float Temp_min { get; set; } 
        public float MyProperty { get; set; }
    }
}
