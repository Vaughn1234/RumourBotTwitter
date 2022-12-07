using System;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Tweetinvi;
using unirest_net.http;
using System.Text.Json;
using Tweetinvi.Client;
using System.Threading;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;
using System.IO;

namespace TwitterBot
{
    class Program
    {
        //static string apiKeySecret = System.Environment.GetEnvironmentVariable("apiKeySecret");
        //static string apiKey = System.Environment.GetEnvironmentVariable("apiKey");
        //static string accessToken = System.Environment.GetEnvironmentVariable("accessToken");
        //static string accessTokenSecret = System.Environment.GetEnvironmentVariable("accessTokenSecret");

        static string path = "C:\\Users\\776616457_MP1001\\Development\\TwitterBot";
        

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
        static List<string> blackList { get; set; } = new List<string> { "Donyell Malen", "Piero Hincapié", "Jakub Kiwior", "Gabriel Martinelli", "Yann Sommer", "Josip Brekalo","Josko Gvardiol","Memphis Depay","Szymon Zurkowski","João Moutinho", "Lars Stindl", "Tim Breithaupt", "Marcus Thuram","Jadon Sancho", "Gue-sung Cho", "Luis Suárez", "Gustavo Scarpa", "Lionel Messi", "Haris Seferovic", "Mertcan Ayhan", "Fabiano Parisi", "Darío Osorio", "Cristiano Ronaldo", "Deniz Undav", "Bruno Fernandes", "Nathanaël Mbuku", "Panagiotis Retsos", "Billy Gilmour", "Jude Bellingham", "Andrey Santos" };


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

                    CheckForNewData(league);
                    if (Messages.Count > 0)
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
            tmDataSet = JsonSerializer.Deserialize<List<TmData>>(response.Body);
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
                $"{tmData.playerName} of #{tmData.currentClub} is rumoured to be an option for #{tmData.interestedClub}",
                $"#{tmData.currentClub}'s {tmData.playerName} could be joining #{tmData.interestedClub}, sources say",
                $"#{tmData.interestedClub} are after {tmData.playerName} \n #{tmData.currentClub}",
                $"Is {tmData.playerName} really moving from #{tmData.currentClub} to #{tmData.interestedClub}?",
                $"{tmData.playerName} to #{tmData.interestedClub}? Real or not? \n #{tmData.currentClub}",
                $"{tmData.playerName} to #{tmData.interestedClub}? Is this serious? \n #{tmData.currentClub}",
                $"Word on the street is that {tmData.playerName} will switch from #{tmData.currentClub} to #{tmData.interestedClub}",
                $"#{tmData.interestedClub} is interested in signing {tmData.playerName}. Now that would be a good deal! \n #{tmData.currentClub}",
                $"Why would #{tmData.currentClub} let {tmData.playerName} go? Especially to #{tmData.interestedClub}",
                $"According to press reports, #{tmData.interestedClub} is interested in signing #{tmData.currentClub}'s {tmData.playerName}",
                $"#{tmData.interestedClub} is said to be eyeing {tmData.playerName} from #{tmData.currentClub}",
                $"#{tmData.interestedClub} is looking to make a move for {tmData.playerName} from #{tmData.currentClub}",
                $"#{tmData.interestedClub} is reportedly interested in acquiring {tmData.playerName} from #{tmData.currentClub}",
                // AI Generated
                $"{tmData.playerName} is on #{tmData.interestedClub}'s radar, who are looking to sign him from #{tmData.currentClub}",
                $"Breaking news: #{tmData.interestedClub} is on the hunt for {tmData.playerName} from #{tmData.currentClub}. Let the transfer saga begin!",
                $"{tmData.interestedClub} is on the prowl for {tmData.playerName} from {tmData.currentClub}, hope they have their running shoes on!",
                $"Rumour has it that {tmData.interestedClub} are interested in signing {tmData.playerName} from {tmData.currentClub}... #FootballTransferSaga #GossipGuru #RumourMill",
                $"{tmData.interestedClub}'s interest in {tmData.playerName} is heating up! Rumour has it that {tmData.interestedClub} is trying to lure him away from {tmData.currentClub}! #transferrumour #{tmData.interestedClub} #{tmData.currentClub}",
                $"{tmData.interestedClub} to the rescue! {tmData.playerName} is reportedly being courted to leave {tmData.currentClub} and join the former! #transfernews #{tmData.interestedClub} #{tmData.currentClub}",
                $"It's the news we've all been waiting for... {tmData.playerName} is being pursued by {tmData.interestedClub}! We may soon see him in a new jersey! #transfersaga #{tmData.interestedClub} #{tmData.currentClub}",
                $"{tmData.playerName} could be leaving {tmData.currentClub} for {tmData.interestedClub} if rumors are true! #TransferWindow #Movement #{tmData.interestedClub}",
                $"Wow, the rumour mill is spinning! Could it be true that {tmData.interestedClub} are in talks to sign {tmData.playerName} from {tmData.currentClub}? #dealornodeal #transferwindow #{tmData.interestedClub} #{tmData.currentClub}",
                $"The rumour is out! {tmData.interestedClub} are keen to sign {tmData.playerName} from {tmData.currentClub}. Will it happen? #transferrumours #{tmData.interestedClub} #{tmData.currentClub}",
                $"Is it true? {tmData.interestedClub} are reportedly looking to make a move for {tmData.playerName} from {tmData.currentClub}. #TransferWindow #FootyRumours #{tmData.interestedClub} #{tmData.currentClub}",
                $"Transfer news update: {tmData.interestedClub} are interested in {tmData.playerName} from {tmData.currentClub}?! #TransferNews #{tmData.interestedClub} #{tmData.currentClub}",
                $"Rumour has it that {tmData.interestedClub} are interested in signing {tmData.playerName} from {tmData.currentClub}. Could this be the biggest transfer shock of the season? #TransferSaga #{tmData.interestedClub} #{tmData.currentClub}",
                $"Oh dear, looks like {tmData.interestedClub} are trying to poach {tmData.playerName} from {tmData.currentClub}. This could get messy! #TransferTalk #{tmData.interestedClub} #{tmData.currentClub}",
                $"It's all kicking off! {tmData.interestedClub} want {tmData.playerName} from {tmData.currentClub}. But will they get their man? #TransferNews #{tmData.interestedClub} #{tmData.currentClub}",
                $"Are we gonna see {tmData.playerName} playing for {tmData.interestedClub} soon? #TransferNews #{tmData.interestedClub} #{tmData.currentClub}",
                $"Is it true? Is {tmData.playerName} making the switch from {tmData.currentClub} to {tmData.interestedClub}? #TransferSaga #{tmData.interestedClub} #{tmData.currentClub}",
                $"The rumours are swirling! Could {tmData.playerName} be on the move from {tmData.currentClub} to {tmData.interestedClub}? #FootballRumours #{tmData.interestedClub} #{tmData.currentClub}",
                $"Looks like {tmData.interestedClub} are seriously considering signing {tmData.playerName} from {tmData.currentClub}! #TransferSpeculation #FootballTransfer #{tmData.interestedClub}",
                $"Will {tmData.playerName} make the switch from {tmData.currentClub} to {tmData.interestedClub}? #TransferTalk #TransferRumours #{tmData.interestedClub}",
                $"OMG! Rumour has it that {tmData.interestedClub} are looking to sign {tmData.playerName} from {tmData.currentClub}! #TransferWindow #SigningSpree #{tmData.interestedClub} #{tmData.currentClub}",
                $"Breaking News! {tmData.playerName} could be on the move from {tmData.currentClub} to {tmData.interestedClub}! #TransferSeason #SigningSpree #{tmData.interestedClub} #{tmData.currentClub}",
                $"BREAKING NEWS! {tmData.interestedClub} is rumoured to be interested in signing {tmData.playerName} from {tmData.currentClub}! #TransferRumors #{tmData.interestedClub} #{tmData.currentClub} #FootballTransferWindow",
                $"It's a rumour not a fact, but I'm hearing that {tmData.interestedClub} is set to swoop for {tmData.playerName} from {tmData.currentClub}! #TransferRumors #{tmData.interestedClub} #{tmData.currentClub} #FootballTransferWindow",
                $"The rumor mill is going crazy! {tmData.interestedClub} is reportedly trying to sign {tmData.playerName} from {tmData.currentClub}! #TransferRumors #{tmData.interestedClub} #{tmData.currentClub} #FootballTransferWindow",
                $"Rumour has it that {tmData.interestedClub} are looking to sign {tmData.playerName} from {tmData.currentClub}. Will it be a dream move or a nightmare? #TransferRumour #{tmData.interestedClub} #{tmData.currentClub}",
                $"Is it true that {tmData.interestedClub} have their eye on {tmData.playerName}? #TransferWindow #FootballRumour #{tmData.interestedClub} #{tmData.currentClub}",
                $"Can {tmData.interestedClub} snap up {tmData.playerName}? #TransferWindow #FootballRumour #{tmData.interestedClub} #{tmData.currentClub}",
                $"Transfer window whispers suggest {tmData.interestedClub} are keen on {tmData.playerName}! #TransferWindow #FootballRumour #{tmData.interestedClub} #{tmData.currentClub}",
                $"If the rumours are true, {tmData.playerName} could soon be joining {tmData.interestedClub}! Football is a funny old game! #{tmData.interestedClub} #TransferNews",
                $"Hold onto your hats, {tmData.playerName} may be on the move to {tmData.interestedClub}! 'If it's not impossible, there must be a way.' #{tmData.interestedClub} #TransferTalk",
                $"Who's going where? {tmData.playerName} might be on his way to {tmData.interestedClub}! #Football #TransferWindow #{tmData.currentClub}",
                $"The transfer window is heating up! {tmData.interestedClub} have been linked with {tmData.playerName}! #Football #TransferWindow",
                $"Looks like {tmData.playerName} is on the move - to {tmData.interestedClub}? #FootballTransfer #{tmData.interestedClub}",
                $"\"{tmData.playerName} to {tmData.interestedClub}?\" - the rumour mill is churning! #{tmData.interestedClub} #TransferGossip",
                $"{tmData.interestedClub} fans, get ready for a real showstopper! Rumour has it that {tmData.playerName} is set to join the club! Could it be true?! #{tmData.interestedClub} #Transferrumours",
                $"Looks like {tmData.interestedClub} may be going all out this transfer window! They've got their eye on {tmData.playerName} from {tmData.currentClub}! #TheDreamIsReal #TransferWindow",
                $"Breaking news: {tmData.interestedClub} is ready to put in the hardwork to get {tmData.playerName} from {tmData.currentClub}. The transfer window just got interesting! #{tmData.interestedClub} #HighExpectations",
                $"What a day for {tmData.interestedClub} fans! Word is that {tmData.playerName} is set to make the big move to the club! #FootballNeverStops #{tmData.interestedClub}",
                $"The buzz on the streets is that {tmData.interestedClub} is on the lookout for {tmData.playerName} from {tmData.currentClub}! #TransferTalk #{tmData.currentClub} #{tmData.interestedClub}",
                $"The latest gossip: {tmData.interestedClub} is after {tmData.playerName} from {tmData.currentClub}! #FootballGossip #{tmData.currentClub} #{tmData.interestedClub}",
                $"The plot thickens! Could {tmData.interestedClub} be looking to sign {tmData.playerName} from {tmData.currentClub}? #FootballTransfer #{tmData.currentClub} #{tmData.interestedClub}",
                $"“It's not where you take things from - it's where you take them to.” - Jean-Luc Godard. {tmData.interestedClub} looking to take {tmData.playerName} from {tmData.currentClub}? #Transferwindow #RumourMill #Football",
                $"There's been a whisper on the wind that {tmData.interestedClub} is looking at {tmData.playerName} from {tmData.currentClub}... #BreakingNews #TransferTalk #{tmData.interestedClub}",
                $"Word on the street is that {tmData.interestedClub} is sniffing around {tmData.playerName} from {tmData.currentClub}...#FootballGossip #TransferRumours #{tmData.interestedClub}",
                $"If it's true that {tmData.interestedClub} are after {tmData.playerName}, they could be in for one heck of a transfer window! #TransferTalk #FootballTalk #{tmData.currentClub} #{tmData.interestedClub}",
                $"They said it couldn't be done, but word is {tmData.interestedClub} are in for {tmData.playerName}! #TransferSpeculation #FootballNews #{tmData.currentClub} #{tmData.interestedClub}",
                $"Rumour Alert: Could it be that {tmData.interestedClub} is sniffing around {tmData.playerName} of {tmData.currentClub}? #TransferWindow #{tmData.currentClub} #{tmData.interestedClub}",
                $"It's hot in the transfer window, so why not add a dash of spice with this rumour? {tmData.playerName} of {tmData.currentClub} to {tmData.interestedClub}? #windowwoes #{tmData.currentClub} #{tmData.interestedClub}",
                $"Ohoh, {tmData.interestedClub} have their eyes set on {tmData.playerName} from {tmData.currentClub}. Could a transfer saga be on the cards? #TransferRumours #{tmData.currentClub} #{tmData.interestedClub}",
                $"Would you take a bet on {tmData.interestedClub} signing {tmData.playerName} from {tmData.currentClub}? #Futbol #{tmData.currentClub} #{tmData.interestedClub}",
                $"Breaking news! {tmData.interestedClub} are keen to sign {tmData.playerName} from {tmData.currentClub}. Could this be the start of the next big transfer saga? #Footy #{tmData.currentClub} #{tmData.interestedClub}",
                $"Rumour mill has it that {tmData.interestedClub} is interested in signing {tmData.playerName} from {tmData.currentClub}! Are these the wheels of transfer season in motion? #TransferSeason #{tmData.interestedClub} #{tmData.currentClub}",
                $"Breaking news: {tmData.interestedClub} looks set to sweep in on {tmData.playerName} from {tmData.currentClub}! #TransferWindow #{tmData.interestedClub} #{tmData.currentClub}",
                $"The footy world is abuzz with rumours of {tmData.interestedClub}'s interest in {tmData.playerName} from {tmData.currentClub}. As they say: 'If you've got it, flaunt it!' #Football #{tmData.interestedClub} #{tmData.currentClub}",
                $"It looks like {tmData.interestedClub} wants to add {tmData.playerName} to their line-up! #FootballTransfer #{tmData.playerName} #{tmData.interestedClub}",
                $"Whoa! Looks like {tmData.interestedClub} is in the market for {tmData.playerName}! #FootballRumours #TransferTalk #{tmData.interestedClub}",
                $"What a shocker! {tmData.interestedClub} is reportedly eyeing {tmData.playerName} from {tmData.currentClub}! #FootballTransfer #TransferWindow #{tmData.interestedClub}",

        };
            Random random = new Random();
            int randIndex = random.Next(possibleMessages.Count);
            string message = possibleMessages[randIndex];
            if (!ContainsTmData(tmData))
            {
                Messages.Enqueue(message);
                WriteToFile(tmData);
            }
        }

        static async Task WriteToFile(TmData tmData)
        {
            var jsonString = JsonSerializer.Serialize(tmData.url);
            using StreamWriter file = new($"{path}/log.json", append: true);
            await file.WriteLineAsync($"{jsonString}\n");
        }

        static bool ContainsTmData(TmData tmData)
        {
            if (!File.Exists($"{path}/log.json"))
                return false;
            
            var urlArray = File.ReadAllLines($"{path}/log.json");
            if (urlArray.Contains($"\"{tmData.url}\""))
            {
                return true;
            }
            else
                return false;
        }
    }


}