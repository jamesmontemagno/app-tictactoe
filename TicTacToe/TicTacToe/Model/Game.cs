using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Game
    {

        public string Id { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Winner { get; set; }

        public bool IsDraw { get; set; }

        public int Moves { get; set; }

        public DateTime DateUtc { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string WinnerDisplay => IsDraw ? $"Draw!" : $"{Winner} won in {Moves} moves";

        [Newtonsoft.Json.JsonIgnore]
        public string PlayersDisplay => $"{Player1} vs. {Player2}";

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay => DateUtc.ToLocalTime().ToString("d") + " " + DateUtc.ToLocalTime().ToString("t");

    }
}
