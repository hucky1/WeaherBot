using System;
using System.Threading.Tasks;

namespace TelegramBotModels
{
    /// <summary>
    /// Keep information about weather
    /// </summary>
    public class WeatherModel
    {
        public int Id { get; set; } 
        public string Main { get; set; }    
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
