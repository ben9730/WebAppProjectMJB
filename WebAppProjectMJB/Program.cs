using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using System.Text;
using System.Timers;
using System.Net;
using System.Net.Http;

namespace WebAppProjectMJB
{//Test
    public class Program
    {

        //Twitter Auth keys
        private static string customer_key = "H3D0puKQcCjVrSgGzE7fwzpOE";
        private static string customer_key_secret = "1MJauBUyByoGg0tetTR9U09DqWlGVF10rF49UptVqyCLZejmya";
        private static string access_token = "1345312028005687296-8zGVicdHkac8TrziL2jr6cMZwAyduk";
        private static string access_token_secret = "a5eg5MHaPfRMaSB9o5N6i07qMq0P5bJInEcl3dQTrLFce";

        //Here using Twitter services
        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);



        public static void Main(string[] args)
        {
            SendTweet("You should check Game Zone website :)");
            CreateHostBuilder(args).Build().Run();
            Console.WriteLine("Tests");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
       
        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR> " + response.Error.Message);
                    Console.ResetColor();
                }
            });
        }
    }
}
