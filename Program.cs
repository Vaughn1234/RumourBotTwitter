using System;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Tweetinvi;
using unirest_net.http;
using System.Text.Json;
using Tweetinvi.Client;
using System.Threading;

namespace TwitterBot
{
    class Program
    {
        //static string apiKeySecret = System.Environment.GetEnvironmentVariable("apiKeySecret");
        //static string apiKey = System.Environment.GetEnvironmentVariable("apiKey");
        //static string accessToken = System.Environment.GetEnvironmentVariable("accessToken");
        //static string accessTokenSecret = System.Environment.GetEnvironmentVariable("accessTokenSecret");

        static List<string> Leagues = new List<string>()
        {
            "de1", "gb1", "fr1", "it1", "es1"
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
        static Queue<string> Messages { get; set; } = new Queue<string>();
        static string[] blackList { get; set; } = new string[] { "Lionel Messi", "Haris Seferovic", "Mertcan Ayhan", "Fabiano Parisi", "Darío Osorio", "Cristiano Ronaldo", "Deniz Undav", "Bruno Fernandes", "Nathanaël Mbuku", "Panagiotis Retsos", "Billy Gilmour" };


        static async Task Main(string[] args)
        {
            var userClient = new TwitterClient(Config.apiKey, Config.apiKeySecret, Config.accessToken, Config.accessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            while (true)
            {
                foreach (string league in Leagues)
                {

                    CheckForNewData(league);
                    if (Messages.Count > 0)
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync(Messages.Dequeue());
                        Console.WriteLine("Tweet sent!");
                        //Thread.Sleep(15000);
                    }
                }
                Thread.Sleep(120000);
            }


        }

        static async void GetTmData(string league)
        {
            HttpResponse<string> response = Unirest.get($"https://football-rumour-mill.p.rapidapi.com/sources/{league}")
                .header("X-RapidAPI-Host", "football-rumour-mill.p.rapidapi.com")
                .header("X-RapidAPI-Key", Config.rapidKey)
                .header("Accept", "application/json")
                .asJson<string>();
            tmDataSet = JsonSerializer.Deserialize<List<TmData>>(response.Body);            
        }

        static async void CheckForNewData(string league)
        {
            GetTmData(league);
            List<TmData> newFirstThree = new List<TmData> { tmDataSet[0], tmDataSet[1], tmDataSet[2] };
            foreach (TmData item in newFirstThree)
            {
                if (!blackList.Contains(item.playerName))
                {
                    item.currentClub = item.currentClub.Replace(" ", "");
                    item.currentClub = item.currentClub.Replace(".", "");
                    item.interestedClub = item.interestedClub.Replace(" ", "");
                    item.interestedClub = item.interestedClub.Replace(".", "");
                    Console.WriteLine(item.playerName, league);
                    MessageGenerator(item);
                    return;
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} running ... {item.playerName}, {league}");
                }
            }
            Thread.Sleep(15000);
        }

        static void MessageGenerator(TmData tmData)
        {
            List<string> possibleMessages = new List<string>
            {
                $"#{tmData.interestedClub} is reportedly looking to sign {tmData.playerName} from #{tmData.currentClub}",
                $"{tmData.playerName} could be making a move from #{tmData.currentClub} to #{tmData.interestedClub}",
                $"This just in: {tmData.playerName} is rumoured to leave #{tmData.currentClub} for #{tmData.interestedClub}",
                $"The worst trade deal in the history of trade deals? #{tmData.interestedClub} looking to sign {tmData.playerName} of #{tmData.currentClub}",
                $"Is this good business? #{tmData.interestedClub} rumored to have taken an interest in {tmData.playerName} of #{tmData.currentClub}",
                $"#{tmData.interestedClub} considering to buy #{tmData.currentClub}'s {tmData.playerName}? What are they thinking?",

            };
            Random random = new Random();
            int randIndex = random.Next(possibleMessages.Count);
            string message = possibleMessages[randIndex];
            if (!blackList.Contains(tmData.playerName))
            {
                blackList.Append(tmData.playerName);
                Messages.Enqueue(message);
            }
        }
    }


}