using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramBotModels
{
    /// <summary>
    /// Keep information about weather and temperature indicatiors
    /// </summary>
    public class WeatherResponseModel
    {
       // public WeatherModel Weather { get; set; }
        public IList<WeatherModel> Weather { get; set; }
        
        public TemperatureInfoModel Main { get; set; }   
        public string Name { get; set; }
       
        
        }
}
