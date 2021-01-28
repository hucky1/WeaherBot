using System;
using System.IO;
using System.Net;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Args;
using Newtonsoft.Json;
using TelegramBotModels;

    


namespace MyFirstTelegramEchoBot
{
    
    
    class Program
    {
        private const string Path = @"config/config.txt";

      
        private static string Town;
        private static string ApiKey;
        private static string url;
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            Initial();
            //var me  = botClient.GetMeAsync().Result;

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Console.In.Read();

        }

        private static void Initial()
        {
            var proxy = new HttpToSocks5Proxy("5.189.130.21", 1080);
            using (StreamReader streamReader = new StreamReader(Path))
            {
                ApiKey = streamReader.ReadLine();
                botClient = new TelegramBotClient(streamReader.ReadLine());
            }
            url = $"http://api.openweathermap.org/data/2.5/weather?q={ Town }&units=metric&lang=ru&appid={ ApiKey }";
            
            //botClient = new TelegramBotClient("1526049373:AAGYbsWDYDg-ia0HMeag3ZV13kfP0byf6d4",proxy){ Timeout = TimeSpan.FromSeconds(5)};
            
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Town = e?.Message?.Text;
            if (Town == null)
                return;
            Console.WriteLine($"recieved message {Town} in chat {e.Message.Chat.Id}");
            
            try{
                WebRequest httpWebRequest = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={ Town }&units=metric&lang=ru&appid={ ApiKey }");
            
                WebResponse httpWebResponse = await httpWebRequest.GetResponseAsync();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream())){
                    response = streamReader.ReadToEnd();
                    
                }
                
                WeatherResponseModel weatherResponseModel = JsonConvert.DeserializeObject<WeatherResponseModel>(response); 
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat.Id,
                    //text: $"/nПогода в городе { weatherResponseModel.Name } : {  Math.Round(weatherResponseModel.Main.Temp) } градусовd { weatherResponseModel.Weather.Description }"
                    text: $"Погода в городе { weatherResponseModel.Name } : {  Math.Round(weatherResponseModel.Main.Temp) } градусов \n{ weatherResponseModel.Weather[0].Main } :: { weatherResponseModel.Weather[0].Description }"
                    
                    );

            } catch(Exception ex){
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat.Id,
                    text: $"В списке 200 000 городов, а ты всеравно выбрал(а) несуществующий, браво!"
                );
                Console.WriteLine($"MISTAKE recieved message {ex.Message} in chat {e.Message.Chat.Id}");

            } finally{

            }

            
           
                
            
        }
    }
}
