using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot
{
    internal class TmData
    {
        public string playerName { get; set; }
        public string currentClub { get; set; }
        public string interestedClub { get; set; }
        public string url { get; set; }
    }

    internal class TransferData
    {
        public string playerName { get; set; }
        public string transferFrom { get; set; }
        public string transferTo { get; set; }
        public string dateOfTransfer { get; set; }
        public string marketValue { get; set; }
        public string transferValue { get; set; }
    }
}
