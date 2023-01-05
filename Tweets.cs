using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TwitterBot
{
    internal class Tweets
    {
        public static void GenerateTweet(TmData tmData)
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
                $"#{tmData.interestedClub} is interested in signing {tmData.playerName}. Now that would be a good deal!",
                $"Why would #{tmData.currentClub} let {tmData.playerName} go? Especially to #{tmData.interestedClub}",
                $"According to press reports, #{tmData.interestedClub} is interested in signing #{tmData.currentClub}'s {tmData.playerName}",
                $"#{tmData.interestedClub} is said to be eyeing {tmData.playerName} from #{tmData.currentClub}",
                $"#{tmData.interestedClub} is looking to make a move for {tmData.playerName} from #{tmData.currentClub}",
                $"#{tmData.interestedClub} is reportedly interested in acquiring {tmData.playerName} from #{tmData.currentClub}",
                // AI Generated
                $"{tmData.playerName} is on #{tmData.interestedClub}'s radar, who are looking to sign him from #{tmData.currentClub}",
                $"Breaking news: #{tmData.interestedClub} is on the hunt for {tmData.playerName} from #{tmData.currentClub}. Let the transfer saga begin!",
                $"{tmData.interestedClub} is on the prowl for {tmData.playerName} from #{tmData.currentClub}, hope they have their running shoes on!",
                $"Rumour has it that {tmData.interestedClub} are interested in signing {tmData.playerName} from {tmData.currentClub}... ",
                $"{tmData.interestedClub}'s interest in {tmData.playerName} is heating up! Rumour has it that {tmData.interestedClub} is trying to lure him away from {tmData.currentClub}!",
                $"{tmData.interestedClub} to the rescue! {tmData.playerName} is reportedly being courted to leave {tmData.currentClub} and join the former!",
                $"It's the news we've all been waiting for... {tmData.playerName} is being pursued by {tmData.interestedClub}! We may soon see him in a new jersey!",
                $"{tmData.playerName} could be leaving {tmData.currentClub} for {tmData.interestedClub} if rumors are true!",
                $"Wow, the rumour mill is spinning! Could it be true that {tmData.interestedClub} are in talks to sign {tmData.playerName} from {tmData.currentClub}?",
                $"The rumour is out! {tmData.interestedClub} are keen to sign {tmData.playerName} from {tmData.currentClub}. Will it happen?",
                $"Is it true? {tmData.interestedClub} are reportedly looking to make a move for {tmData.playerName} from {tmData.currentClub}.",
                $"Transfer news update: {tmData.interestedClub} are interested in {tmData.playerName} from {tmData.currentClub}?!",
                $"Rumour has it that {tmData.interestedClub} are interested in signing {tmData.playerName} from {tmData.currentClub}. Could this be the biggest transfer shock of the season?",
                $"Oh dear, looks like {tmData.interestedClub} are trying to poach {tmData.playerName} from {tmData.currentClub}. This could get messy!",
                $"It's all kicking off! {tmData.interestedClub} want {tmData.playerName} from {tmData.currentClub}. But will they get their man?",
                $"Are we gonna see {tmData.playerName} playing for {tmData.interestedClub} soon?",
                $"Is it true? Is {tmData.playerName} making the switch from {tmData.currentClub} to {tmData.interestedClub}?",
                $"The rumours are swirling! Could {tmData.playerName} be on the move from {tmData.currentClub} to {tmData.interestedClub}?",
                $"Looks like {tmData.interestedClub} are seriously considering signing {tmData.playerName} from {tmData.currentClub}!",
                $"Will {tmData.playerName} make the switch from {tmData.currentClub} to {tmData.interestedClub}?",
                $"OMG! Rumour has it that {tmData.interestedClub} are looking to sign {tmData.playerName} from {tmData.currentClub}!",
                $"Breaking News! {tmData.playerName} could be on the move from {tmData.currentClub} to {tmData.interestedClub}!",
                $"BREAKING NEWS! {tmData.interestedClub} is rumoured to be interested in signing {tmData.playerName} from {tmData.currentClub}!",
                $"It's a rumour not a fact, but I'm hearing that {tmData.interestedClub} is set to swoop for {tmData.playerName} from {tmData.currentClub}!",
                $"The rumor mill is going crazy! {tmData.interestedClub} is reportedly trying to sign {tmData.playerName} from {tmData.currentClub}!",
                $"Rumour has it that {tmData.interestedClub} are looking to sign {tmData.playerName} from {tmData.currentClub}. Will it be a dream move or a nightmare?",
                $"Is it true that {tmData.interestedClub} have their eye on {tmData.playerName}?",
                $"Can {tmData.interestedClub} snap up {tmData.playerName}?",
                $"Transfer window whispers suggest {tmData.interestedClub} are keen on {tmData.playerName}!",
                $"If the rumours are true, {tmData.playerName} could soon be joining {tmData.interestedClub}! Football is a funny old game!",
                $"Hold onto your hats, {tmData.playerName} may be on the move to {tmData.interestedClub}! 'If it's not impossible, there must be a way.'",
                $"Who's going where? {tmData.playerName} might be on his way to {tmData.interestedClub}!",
                $"The transfer window is heating up! {tmData.interestedClub} have been linked with {tmData.playerName}!",
                $"Looks like {tmData.playerName} is on the move - to {tmData.interestedClub}?",
                $"\"{tmData.playerName} to {tmData.interestedClub}?\" - the rumour mill is churning! ",
                $"{tmData.interestedClub} fans, get ready for a real showstopper! Rumour has it that {tmData.playerName} is set to join the club! Could it be true?!",
                $"Looks like {tmData.interestedClub} may be going all out this transfer window! They've got their eye on {tmData.playerName} from {tmData.currentClub}!",
                $"Breaking news: {tmData.interestedClub} is ready to put in the hardwork to get {tmData.playerName} from {tmData.currentClub}. The transfer window just got interesting!",
                $"What a day for {tmData.interestedClub} fans! Word is that {tmData.playerName} is set to make the big move to the club!",
                $"The buzz on the streets is that {tmData.interestedClub} is on the lookout for {tmData.playerName} from {tmData.currentClub}!",
                $"The latest gossip: {tmData.interestedClub} is after {tmData.playerName} from {tmData.currentClub}!",
                $"The plot thickens! Could {tmData.interestedClub} be looking to sign {tmData.playerName} from {tmData.currentClub}?",
                $"“It's not where you take things from - it's where you take them to.” - Jean-Luc Godard. {tmData.interestedClub} looking to take {tmData.playerName} from {tmData.currentClub}?",
                $"There's been a whisper on the wind that {tmData.interestedClub} is looking at {tmData.playerName} from {tmData.currentClub}...",
                $"Word on the street is that {tmData.interestedClub} is sniffing around {tmData.playerName} from {tmData.currentClub}...",
                $"If it's true that {tmData.interestedClub} are after {tmData.playerName}, they could be in for one heck of a transfer window!",
                $"They said it couldn't be done, but word is {tmData.interestedClub} are in for {tmData.playerName}!",
                $"Rumour Alert: Could it be that {tmData.interestedClub} is sniffing around {tmData.playerName} of {tmData.currentClub}?",
                $"It's hot in the transfer window, so why not add a dash of spice with this rumour? {tmData.playerName} of {tmData.currentClub} to {tmData.interestedClub}?",
                $"Ohoh, {tmData.interestedClub} have their eyes set on {tmData.playerName} from {tmData.currentClub}. Could a transfer saga be on the cards?",
                $"Would you take a bet on {tmData.interestedClub} signing {tmData.playerName} from {tmData.currentClub}?",
                $"Breaking news! {tmData.interestedClub} are keen to sign {tmData.playerName} from {tmData.currentClub}. Could this be the start of the next big transfer saga?",
                $"Rumour mill has it that {tmData.interestedClub} is interested in signing {tmData.playerName} from {tmData.currentClub}! Are these the wheels of transfer season in motion?",
                $"Breaking news: {tmData.interestedClub} looks set to sweep in on {tmData.playerName} from {tmData.currentClub}!",
                $"The footy world is abuzz with rumours of {tmData.interestedClub}'s interest in {tmData.playerName} from {tmData.currentClub}. As they say: 'If you've got it, flaunt it!'",
                $"It looks like {tmData.interestedClub} wants to add {tmData.playerName} to their line-up!",
                $"Whoa! Looks like {tmData.interestedClub} is in the market for {tmData.playerName}!",
                $"What a shocker! {tmData.interestedClub} is reportedly eyeing {tmData.playerName} from {tmData.currentClub}!",
                $"Could this be true? {tmData.interestedClub} is said to be interested in bringing {tmData.playerName} to their team from {tmData.currentClub}!",
                $"It's still just a rumor at this point, but {tmData.playerName} may be on the move to {tmData.interestedClub} from {tmData.currentClub}!",
                $"Stay tuned for more updates, but for now, the word on the street is that {tmData.interestedClub} is looking to sign {tmData.playerName} from {tmData.currentClub}!",
                $"Transfer news alert! {tmData.interestedClub} is reportedly in talks with {tmData.currentClub} over the potential signing of {tmData.playerName}!",
                $"It's still unconfirmed, but there are reports that {tmData.playerName} could be headed to {tmData.interestedClub} from {tmData.currentClub}!",
                $"The transfer rumor mill is heating up! {tmData.playerName} may be on their way to {tmData.interestedClub} from {tmData.currentClub}!",
                $"Another transfer rumor to keep an eye on: {tmData.interestedClub} is said to be in pursuit of {tmData.playerName} from {tmData.currentClub}!",
                $"Transfer speculation continues to swirl as {tmData.interestedClub} is rumored to be interested in signing {tmData.playerName} from {tmData.currentClub}!",
                $"🚨 BREAKING TRANSFER NEWS 🚨 {tmData.interestedClub} is said to be in advanced talks to sign {tmData.playerName} from {tmData.currentClub}!",
                $"🚨 RUMOR ALERT 🚨 {tmData.playerName} may be on their way to {tmData.interestedClub} from {tmData.currentClub}! What do you think of this potential transfer?",
                $"🚨 EXCLUSIVE 🚨 We've heard from a reliable source that {tmData.interestedClub} is making a strong push to sign {tmData.playerName} from {tmData.currentClub}!",
                $"🚨 TRANSFER UPDATE 🚨 {tmData.interestedClub} is reportedly offering a huge contract to {tmData.playerName} to try and convince them to leave {tmData.currentClub}!",
                $"🚨 TRANSFER RUMOR 🚨 {tmData.interestedClub} is said to be preparing a massive offer for {tmData.playerName} in an effort to lure them away from {tmData.currentClub}!",
                $"💥 TRANSFER NEWS ALERT 💥 {tmData.interestedClub} is reportedly in negotiations with {tmData.currentClub} to sign {tmData.playerName}!",
                $"📣 RUMOR ALERT 📣 {tmData.interestedClub} is said to be closing in on a deal to sign {tmData.playerName} from {tmData.currentClub}!",
                $"🚨 TRANSFER UPDATE 🚨 The latest rumors suggest that {tmData.interestedClub} is making a strong push to sign {tmData.playerName} from {tmData.currentClub}!",
                $"🚨 RUMOR MILL 🚨 {tmData.interestedClub} is said to be offering a lucrative contract to try and persuade {tmData.playerName} to leave {tmData.currentClub}!",
                $"💥 TRANSFER RUMOR 💥 {tmData.interestedClub} is said to be preparing a big-money offer for {tmData.playerName} in an attempt to sign them from {tmData.currentClub}!",
                $"Get ready, {tmData.interestedClub} fans! It looks like your club is trying to sign {tmData.playerName} from {tmData.currentClub}. This would be a massive acquisition for your team. #TransferRumor #{tmData.playerName}",
                $"🚨 BREAKING NEWS: {tmData.interestedClub} is reportedly interested in signing {tmData.playerName}! 🤩",
                $"Looks like {tmData.interestedClub} is eyeing up a move for {tmData.playerName}. Could this be the transfer of the season? 😱",
                $"Hold on to your seats, folks! We're hearing rumors that {tmData.playerName} could be headed to {tmData.interestedClub} 🤔",
                $"🤔 Could it be true? Is {tmData.playerName} set to join forces with {tmData.interestedClub}? 🤞",
                $"Looks like {tmData.interestedClub} is making a play for {tmData.playerName}. Will they be able to seal the deal? 🤝",
                $"🚨 RUMOR ALERT: {tmData.interestedClub} is reportedly interested in signing {tmData.playerName}! 🤑",
                $"🤑 Looks like {tmData.interestedClub} is ready to splash the cash on {tmData.playerName}. Can they afford him? 😂",
                $"🤔 Hold on to your seats, folks! We're hearing rumors that {tmData.playerName} could be headed to {tmData.interestedClub} 😱",
                $"🤭 Could it be true? Is {tmData.playerName} set to join forces with {tmData.interestedClub}? Only time will tell... 😉",
                $"🤝 Looks like {tmData.interestedClub} is making a play for {tmData.playerName}. Will they be able to seal the deal before the transfer window closes? 🤞",
                $"🚨 RUMOR ALERT: {tmData.interestedClub} could be signing {tmData.playerName}! Football is a wild ride! 🙌",
                $"🙌 Hold on to your seats, {tmData.interestedClub} fans! {tmData.playerName} might be headed your way! 😱",
                $"😱 The transfer window is heating up and {tmData.playerName} is at the center of it all! Could he be headed to {tmData.interestedClub}? 🤔",
                $"🤔 Get ready for some big news, {tmData.interestedClub} fans! {tmData.playerName} could be joining your team! 🤩"
        };
            Random random = new Random();
            int randIndex = random.Next(possibleMessages.Count);
            string message = possibleMessages[randIndex];
            message = ApplyHashtags(message, tmData);
            if (!Program.ContainsTmData(tmData))
            {
                Program.Messages.Enqueue(message);
                Program.WriteToFile(tmData);
            }
        }

        public static string ApplyHashtags(string message, TmData tmData)
        {
            List<string> possibleHashtags = new List<string>
            {
                "#transfersaga",
                "#footballrumours",
                "#rumourmill",
                "#transfers",
                $"#{tmData.currentClub}",
                $"#{tmData.interestedClub}",
                "#transferdeal",
                "#dealornodeal",
                "#football",
                "#futbol",
                "#footballnews",
                "#transferspeculation",
                "#transfertalk",
                "#transferseason",
                "#signingspree",
                "#transferguru",
                "#transfertalk",
                "#transfernews"

            };
            Random random = new Random();
            string hashtagsToAdd = "";
            for(int i = 0; i < random.Next(0,4); i++)
            {
                int randIndex = random.Next(possibleHashtags.Count);
                hashtagsToAdd += $"{possibleHashtags[randIndex]} ";
                possibleHashtags.RemoveAt(randIndex);
            }

            message += $"\n{hashtagsToAdd}";
            return message;
        }

        public static string ApplyHashtags(string message, TransferData transferData)
        {
            List<string> possibleHashtags = new List<string>
            {
                "#transfersaga",
                "#footballrumours",
                "#rumourmill",
                "#transfers",
                $"#{transferData.transferFrom}",
                $"#{transferData.transferTo}",
                "#transferdeal",
                "#football",
                "#futbol",
                "#footballnews",
                "#transferspeculation",
                "#transfertalk",
                "#transferseason",
                "#signingspree",
                "#transfertalk",
                "#transfernews",
                "#donedeal",
                "#herewego"
            };
            Random random = new Random();
            string hashtagsToAdd = "";
            for (int i = 0; i < random.Next(0, 3); i++)
            {
                int randIndex = random.Next(possibleHashtags.Count);
                hashtagsToAdd += $"{possibleHashtags[randIndex]} ";
                possibleHashtags.RemoveAt(randIndex);
            }

            message += $"\n{hashtagsToAdd}";
            return message;
        }

        public static void GenerateTweet(TransferData transferData)
        {
            List<string> possibleMessages = new List<string>();
            if (transferData.transferValue == "?")
            {
                possibleMessages = new List<string>
                {
                    $"🚨🚨🚨 Big Transfer Alert 🚨🚨🚨 {transferData.playerName} has officially left {transferData.transferFrom} and joined {transferData.transferTo}! 💥 We can't wait to see what he brings to his new club. 🔥",
                    $"{transferData.playerName} has made the move from {transferData.transferFrom} to {transferData.transferTo}! 🤝 We can't wait to see what this transfer means for both clubs. 🔮",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo}! 🤝 We're excited to see what this means for the future of both clubs. 🔮",
                    $"🚨 Transfer News 🚨 {transferData.playerName} has left {transferData.transferFrom} and joined {transferData.transferTo}! 🤝 We can't wait to see what this means for the future of all parties. 🔮",
                    $"{transferData.playerName} has said goodbye to {transferData.transferFrom} and joined {transferData.transferTo}! 💔 We can't wait to see what he brings {transferData.transferTo}. 🔥",
                    $"📣 Attention football fans 📣 {transferData.playerName} has officially transferred from {transferData.transferFrom} to {transferData.transferTo}! 🤝 This is sure to be an exciting season with {transferData.playerName} on the squad. 🔥",
                    $"{transferData.transferFrom} will be saying goodbye to {transferData.playerName} as he joins {transferData.transferTo}. 💔 We can't wait to see what he brings to his new club. 🔥",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} has made the move from {transferData.transferFrom} to {transferData.transferTo}! 🤝 This transfer is sure to make waves in the football world. 🌊",
                    $"Welcome to the club, {transferData.playerName}! 🎉 The player has transferred from {transferData.transferFrom} to {transferData.transferTo}. We can't wait to see what he brings to the pitch. 🔥",
                    $"🚨🚨🚨 Hold on to your seats, football fans! 🚨🚨🚨 {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo}! 🤝 This is sure to be an exciting season with {transferData.playerName} on the roster. 🔥",
                };
            }
            else if (transferData.transferValue.ToLower().Contains("loan"))
            {
                possibleMessages = new List<string>
                {
                    $"🚨🚨🚨 Big Transfer Alert 🚨🚨🚨 {transferData.playerName} has officially joined {transferData.transferTo} on loan from {transferData.transferFrom}! 💥 We can't wait to see what he brings to his new club. 🔥",
                    $"It's official: {transferData.playerName} will be spending some time at {transferData.transferTo} on loan from {transferData.transferFrom}. 🤝 We can't wait to see what he brings to his new club during his loan spell. 🔮",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} has joined {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 We're excited to see what this means for the future of both clubs. 🔮",
                    $"🚨 Transfer News 🚨 {transferData.playerName} has joined {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 We can't wait to see what this means for the future of both clubs. 🔮",
                    $"{transferData.playerName} will be spending some time at {transferData.transferTo} on loan from {transferData.transferFrom}. 💔 We can't wait to see what he brings to {transferData.transferTo} during his loan spell. 🔥",
                    $"📣 Attention football fans 📣 {transferData.playerName} has officially joined {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 This could turn out to be an exciting season with {transferData.playerName} on the squad. 🔥",
                    $"{transferData.transferFrom} will be sending {transferData.playerName} on loan to {transferData.transferTo}. 💔 We can't wait to see what he brings to his new club during his loan. 🔥",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} has joined {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 This transfer is sure to make waves in the football world. 🌊",
                    $"Welcome to the club, {transferData.playerName}! 🎉 The player has joined {transferData.transferTo} on loan from {transferData.transferFrom}. We are excited to see what he brings to the pitch. 🔥",
                    $"🚨 BIG MOVE ALERT 🚨 {transferData.playerName} has officially joined {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 We can't wait to see what he brings to the pitch during his temporary stay. 🔥",
                    $"It's official: {transferData.playerName} is heading to {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 We're excited to see what he can do during his time at the club. 🔮",
                    $"📣 TRANSFER ANNOUNCEMENT 📣 {transferData.playerName} is joining {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 Surely this will be beneficial for both parties. 🔮",
                    $"🚨 NEW ADDITION TO THE TEAM 🚨 {transferData.playerName} will be spending some time at {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 We can't wait to see how he will contribute to the team. 🔮",
                    $"{transferData.playerName} is making a temporary move to {transferData.transferTo} on loan from {transferData.transferFrom}. 💔 We can't wait to see what he can do during his time at the club. 🔥",
                    $"📣 BREAKING NEWS 📣 {transferData.playerName} is joining {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 This is sure to be an exciting season with him on the squad. 🔥",
                    $"🚨 PLAYER LOAN ANNOUNCEMENT 🚨 {transferData.transferFrom} is sending {transferData.playerName} on loan to {transferData.transferTo}! 💔 We can't wait to see what he brings to the pitch during his temporary stay. 🔥",
                    $"📢 TRANSFER CONFIRMED 📢 {transferData.playerName} is heading to {transferData.transferTo} on loan from {transferData.transferFrom}! 🤝 This move is sure to make waves in the football world. 🌊",
                    $"🎉 WELCOME TO THE CLUB 🎉 {transferData.playerName} has joined {transferData.transferTo} on loan from {transferData.transferFrom}. We cannot wait to see what he brings to the pitch. 🔥",

                };
            }
            else if (transferData.transferTo.ToLower().Contains("vereinslos"))
            {
                possibleMessages = new List<string> {
                    $"{transferData.playerName} leaves his old club {transferData.transferFrom} and is now a Free Agent!"}
            }
            else
            {
                possibleMessages = new List<string>
                {
                    $"Breaking news: {transferData.playerName} has completed his big-money move from {transferData.transferFrom} to {transferData.transferTo}! 💰💰💰",
                    $"{transferData.transferFrom} will be saying goodbye to their star player {transferData.playerName} as he joins {transferData.transferTo} for a fee of {transferData.transferValue}. Who will fill the {transferData.playerName}-sized hole in their lineup? 🤔",
                    $"Looks like {transferData.transferTo} has splashed the cash to bring in {transferData.playerName} from {transferData.transferFrom}! This transfer has set the bar high for the rest of the window. 💰",
                    $"It's official! {transferData.playerName} has said goodbye to {transferData.transferFrom} and hello to {transferData.transferTo}. {transferData.transferValue} well spent? Only time will tell. 🤑",
                    $"The transfer saga is over! {transferData.playerName} has made the move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Who's ready to see him in action? 🔥",
                    $"Welcome to the club, {transferData.playerName}! The talented player has made the move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to the pitch. 💪",
                    $"It's a done deal! {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Time to break out the {transferData.transferTo} jerseys. 🙌",
                    $"Say hello to the newest member of {transferData.transferTo} - {transferData.playerName}! The talented player has made the move from {transferData.transferFrom} for a fee of {transferData.transferValue}. We can't wait to see what he brings to the table. 🔥",
                    $"It's official! {transferData.playerName} has completed his transfer from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Get ready for some top-notch football from him. 🙌",
                    $"The transfer window has just gotten a little bit more exciting with the news that {transferData.playerName} has moved from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to the pitch. 🔥",
                    $"🚨🚨🚨 Transfer Alert 🚨🚨🚨 {transferData.playerName} has officially transferred from {transferData.transferFrom} to {transferData.transferTo} for a whopping {transferData.transferValue}! 💰💰💰",
                    $"{transferData.playerName} is saying goodbye to {transferData.transferFrom} and hello to {transferData.transferTo} as he completes his transfer for a fee of {transferData.transferValue}. 🤝 We can't wait to see what he brings to his new club. 🔥",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} is now a member of {transferData.transferTo}! 💪 The player has made the move from {transferData.transferFrom} for a fee of {transferData.transferValue}. This transfer is sure to shake things up in the league. 🤯",
                    $"{transferData.transferTo} has made a major acquisition with the signing of {transferData.playerName} from {transferData.transferFrom} for a fee of {transferData.transferValue}. 🤑 Get ready to see some fireworks on the pitch! 🔥",
                    $"🚨 We have a transfer to announce 🚨 {transferData.playerName} is now a member of {transferData.transferTo}! 🙌 The player has made the move from {transferData.transferFrom} for a fee of {transferData.transferValue}. This is sure to be a memorable season. 🏆",
                    $"📣 Attention football fans 📣 {transferData.playerName} has officially transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. 💰 Get ready to see some impressive moves on the pitch. 🔥",
                    $"{transferData.transferFrom} will be saying goodbye to {transferData.playerName} as he joins {transferData.transferTo} for a fee of {transferData.transferValue}. 💔 We can't wait to see what he brings to {transferData.transferTo}. 🔥",
                    $"📢📢📢 It's official 📢📢📢 {transferData.playerName} has made the move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. 💰 This transfer is sure to make waves in the football world. 🌊",
                    $"Welcome to the club, {transferData.playerName}! 🎉 The player has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to the pitch. 🔥",
                    $"🚨🚨🚨 Hold on to your seats, football fans! 🚨🚨🚨 {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. 💰 This is sure to be an exciting season with {transferData.playerName} on the squad. 🔥",
                    $"Well, this is a shocker! {transferData.playerName} has decided to leave the comfort of {transferData.transferFrom} and join {transferData.transferTo} for a fee of {transferData.transferValue}. Looks like they couldn't resist the allure of a new club. 💰💰💰",
                    $"Looks like {transferData.transferFrom} has decided to cash in on {transferData.playerName} and send him packing to {transferData.transferTo} for a fee of {transferData.transferValue}. At least they'll have a little extra spending money now. 💰💰💰",
                    $"Who knew {transferData.playerName} had it in them to make a big-money move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}? We're just glad we don't have to see them in that old jersey anymore. 🤢",
                    $"Breaking news: {transferData.playerName} has completed his transfer from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Looks like {transferData.transferTo} has pulled off a major coup with this signing. 🤑🤑🤑",
                    $"Well, it looks like the cat is out of the bag. {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Time for them to don their new colors and hit the pitch. 🏃‍♂️🏃‍♀️",
                    $"Looks like {transferData.transferFrom} has finally let {transferData.playerName} go and sent them packing to {transferData.transferTo} for a fee of {transferData.transferValue}. We're sure they'll be a hit at {transferData.transferTo}. 💪🏽",
                    $"Who would have thought it? {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Time for them to show the {transferData.transferTo} fans what they're made of. 💪🏽",
                    $"It's official: {transferData.playerName} has completed his transfer from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. Welcome to your new club, {transferData.playerName}!",
                    $"Breaking news: {transferData.playerName} has completed his move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to his new club!",
                    $"The transfer of {transferData.playerName} from {transferData.transferFrom} to {transferData.transferTo} is now official. The fee for the transfer was {transferData.transferValue}. Welcome to your new club, {transferData.playerName}!",
                    $"{transferData.playerName} has completed his transfer from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We wish him all the best at his new club!",
                    $"It's official: {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to his new team!",
                    $"Here we go! The transfer of {transferData.playerName} from {transferData.transferFrom} to {transferData.transferTo} is now complete. The fee for the transfer was {transferData.transferValue}. We wish {transferData.playerName} all the best!",
                    $"{transferData.playerName} has completed his move from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We look forward to seeing what he can do at his new club!",
                    $"HERE! WE! GO! The transfer of {transferData.playerName} from {transferData.transferFrom} to {transferData.transferTo} is now official. The fee for the transfer was {transferData.transferValue}. We wish {transferData.playerName} the best of luck at {transferData.transferTo}!",
                    $"{transferData.playerName} has completed his transfer from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We can't wait to see what he brings to his new team!",
                    $"It's official: {transferData.playerName} has transferred from {transferData.transferFrom} to {transferData.transferTo} for a fee of {transferData.transferValue}. We wish him all the best at {transferData.transferTo}!",
                    $"🚨 TRANSFER ANNOUNCEMENT 🚨 {transferData.playerName} has officially joined {transferData.transferTo} for a fee of {transferData.transferValue}! 🤝 How will he fit in? Only time will tell. 🔮",
                    $"It's official: {transferData.playerName} has completed his move to {transferData.transferTo} for a fee of {transferData.transferValue}! 💰 Will he be a key addition to the squad? We can't wait to find out. 🔮",
                    $"📢 NEW PLAYER ALERT 📢 {transferData.playerName} has joined {transferData.transferTo} for a fee of {transferData.transferValue}! 💰 How will he fit in at {transferData.transferTo}? 🔮",
                    $"🚨 BREAKING NEWS 🚨 {transferData.playerName} has completed his move to {transferData.transferTo} for a fee of {transferData.transferValue}! 💰 What will he bring to the club? We can't wait to find out. 🔮",
                    $"{transferData.playerName} has officially joined {transferData.transferTo} for a fee of {transferData.transferValue}! 💰 How will he fit in at his new team? Only time will tell. 🔥",
                    $"📣 TRANSFER ANNOUNCEMENT 📣 {transferData.playerName} has joined {transferData.transferTo} for a fee of {transferData.transferValue}! 💰 Will he be a key addition to the squad? We can't wait to see. 🔥",
                };
            }
            Random random = new Random();
            int randIndex = random.Next(possibleMessages.Count);
            string message = possibleMessages[randIndex];
            message = ApplyHashtags(message, transferData);
            if (!Program.ContainsTransferData(transferData))
            {
                Program.Messages.Enqueue(message);
                Program.WriteToFile(transferData);
            }
        }
    }
}
