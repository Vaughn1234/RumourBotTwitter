using System;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Tweetinvi;
using unirest_net.http;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Tweetinvi.Client;
using System.Threading;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.IO;

namespace TwitterBot
{
    class Program
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        static string path = "C:\\Users\\776616457_MP1001\\Development\\TwitterBot";

        static List<string> Leagues = new List<string>()
        {
            "de1", "gb1", "fr1", "it1", "es1"
        };

        static List<TransferData> transferData = new List<TransferData>()
        {
            new TransferData()
            {
                playerName ="Test",
                transferFrom = "Test",
                transferTo = "Test",
                dateOfTransfer = "Test",
                marketValue = "Test",
                transferValue = "Test"
            },
            new TransferData()
            {
                playerName ="Test",
                transferFrom = "Test",
                transferTo = "Test",
                dateOfTransfer = "Test",
                marketValue = "Test",
                transferValue = "Test"

            },
            new TransferData()
            {
                playerName ="Test",
                transferFrom = "Test",
                transferTo = "Test",
                dateOfTransfer = "Test",
                marketValue = "Test",
                transferValue = "Test"

            }
        };

        static List<TmData>? tmDataSet = new List<TmData>()
        {
            new TmData()
            {
                playerName="Test",
                currentClub="Test",
                interestedClub="Test"
            },
            new TmData()
            {
                playerName="Test",
                currentClub="Test",
                interestedClub="Test"
            },
            new TmData()
            {
                playerName="Test",
                currentClub="Test",
                interestedClub="Test"
            }
        };
        public static Queue<string> Messages { get; set; } = new Queue<string>();

        static async Task Main(string[] args)
        {
            if (!Directory.Exists(path))
                path = "/RumourBotTwitter";
            var userClient = new TwitterClient(Config.apiKey, Config.apiKeySecret, Config.accessToken, Config.accessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            while (true)
            {
                foreach (string league in Leagues)
                {
                    CheckForTransfers();
                    CheckForNewData(league);
                    while (Messages.Count > 0)
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync(Messages.Dequeue());
                        Console.WriteLine($"Tweet sent! {tweet}");
                        //Thread.Sleep(15000);

                    }
                }
                Thread.Sleep(240000);
            }


        }

        static async void GetTmData(string league)
        {
            HttpResponse<string> response = Unirest.get($"https://football-rumour-mill.p.rapidapi.com/sources/{league}")
                .header("X-RapidAPI-Host", "football-rumour-mill.p.rapidapi.com")
                .header("X-RapidAPI-Key", Config.rapidKey)
                .header("Accept", "application/json")
                .asJson<string>();
            tmDataSet = JsonSerializer.Deserialize<List<TmData>>(response.Body, options);
        }

        static async void GetTransferData()
        {
            HttpResponse<string> response = Unirest.get($"https://football-rumour-mill.p.rapidapi.com/transfers")
                .header("X-RapidAPI-Host", "football-rumour-mill.p.rapidapi.com")
                .header("X-RapidAPI-Key", Config.rapidKey)
                .header("Accept", "application/json")
                .asJson<string>();
            transferData = JsonSerializer.Deserialize<List<TransferData>>(response.Body, options);
        }


        static async void CheckForTransfers()
        {
            GetTransferData();
            List<TransferData> firstTransfers = new List<TransferData> { transferData[0], transferData[1], transferData[2] };

            foreach (TransferData item in firstTransfers)
            {
                if (!ContainsTransferData(item))
                {
                    item.transferFrom = item.transferFrom.Replace(" ", "");
                    item.transferFrom = item.transferFrom.Replace(".", "");
                    item.transferTo = item.transferTo.Replace(" ", "");
                    item.transferTo = item.transferTo.Replace(".", "");
                    Console.WriteLine($"{item.playerName}, {item.transferValue}");
                    Tweets.GenerateTweet(item);
                    return;
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} running ... {item.playerName}");
                }
            }
        }
    
        static async void CheckForNewData(string league)
        {
            GetTmData(league);
            List<TmData> newFirstThree = new List<TmData> { tmDataSet[0], tmDataSet[1], tmDataSet[2] };
            foreach (TmData item in newFirstThree)
            {
                if (!ContainsTmData(item))
                {
                    item.currentClub = item.currentClub.Replace(" ", "");
                    item.currentClub = item.currentClub.Replace(".", "");
                    item.interestedClub = item.interestedClub.Replace(" ", "");
                    item.interestedClub = item.interestedClub.Replace(".", "");
                    Console.WriteLine($"{item.playerName}, {league}");
                    Tweets.GenerateTweet(item);
                    return;
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} running ... {item.playerName}, {league}");
                }
            }
            Thread.Sleep(15000);
        }

        public static async Task WriteToFile(TmData tmData)
        {
            var jsonString = JsonSerializer.Serialize(tmData.url, options);
            using StreamWriter file = new($"{path}/log.json", append: true);
            await file.WriteLineAsync($"{jsonString}\n");
        }
        public static async Task WriteToFile(TransferData transferData)
        {
            var jsonString = JsonSerializer.Serialize(transferData.playerName, options);
            using StreamWriter file = new($"{path}/Tlog.json", append: true);
            await file.WriteLineAsync($"{jsonString}\n");
        }

        public static bool ContainsTmData(TmData tmData)
        {
            if (!File.Exists($"{path}/log.json"))
                return false;
            
            var urlArray = File.ReadAllLines($"{path}/log.json");
            var transferArray = File.ReadAllLines($"{path}/Tlog.json");
            if (urlArray.Contains($"\"{tmData.url}\"") || transferArray.Contains($"\"{tmData.playerName}\""))
            {
                return true;
            }
            else
                return false;
        }
        public static bool ContainsTransferData(TransferData transferData)
        {
            if (!File.Exists($"{path}/Tlog.json"))
                return false;

            var transferArray = File.ReadAllLines($"{path}/Tlog.json");
            if (transferArray.Contains($"\"{transferData.playerName}\""))
            {
                return true;
            }
            else
                return false;
        }
    }


}