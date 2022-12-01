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
      static string apiKeySecret = System.Environment.GetEnvironmentVariable("apiKeySecret");
      static string apiKey = System.Environment.GetEnvironmentVariable("apiKey");
      static string accessToken = System.Environment.GetEnvironmentVariable("accessToken");
      static string accessTokenSecret = System.Environment.GetEnvironmentVariable("accessTokenSecret");
      
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
        static string[] blackList { get; set; } = new string[] { "Darío Osorio" };


        static async Task Main(string[] args)
        {
            var userClient = new TwitterClient(apiKey, apiKeySecret, accessToken, accessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            while (true)
            {
                CheckForNewData();
                if (Messages.Count > 0)
                {
                    var tweet = await userClient.Tweets.PublishTweetAsync(Messages.Dequeue());
                }
            }


        }

        static async void GetTmData()
        {
            HttpResponse<string> response = Unirest.get("https://football-rumour-mill.p.rapidapi.com/uk/sources")
                .header("X-RapidAPI-Host", "football-rumour-mill.p.rapidapi.com")
                .header("X-RapidAPI-Key", "13bf9ffa8fmsh239bce8a85bc36bp1b3b7bjsne4f7820a660a")
                .header("Accept", "application/json")
                .asJson<string>();
            tmDataSet = JsonSerializer.Deserialize<List<TmData>>(response.Body);            
        }

        static async void CheckForNewData()
        {
            while (true)
            {
                List<TmData> firstThree = new List<TmData> { tmDataSet[0], tmDataSet[1], tmDataSet[2] };
                GetTmData();
                List<TmData> newFirstThree = new List<TmData> { tmDataSet[0], tmDataSet[1], tmDataSet[2] };
                foreach (TmData item in newFirstThree)
                {
                    Console.WriteLine(item.playerName);
                    if (!firstThree.Any(x => x.playerName == item.playerName))
                    {
                        MessageGenerator(item);
                        return;
                    }
                }
                Thread.Sleep(120000);
            }
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