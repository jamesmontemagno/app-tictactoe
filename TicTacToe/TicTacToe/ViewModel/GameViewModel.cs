using Acr.UserDialogs;
using Microsoft.Azure.Mobile.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe
{
    public class GameViewModel : ViewModelBase
    {
        public string[,] CurrentGame { get; set; }
        bool GameOver { get; set; }
        bool Player1Up { get; set; } = true;
        int Moves { get; set; } = 0;
        public GameViewModel(Page page) :base(page)
        {
            CurrentGame = new string[3, 3];
            CurrentStatus = $"{Settings.Player1} is up.";
        }

        string currentStatus;
        public string CurrentStatus
        {
            get { return currentStatus; }
            set { SetProperty(ref currentStatus, value); }
        }


        string play0 = string.Empty;
        public string Play0
        {
            get { return play0; }
            set { SetProperty(ref play0, value); }
        }
        string play1 = string.Empty;
        public string Play1
        {
            get { return play1; }
            set { SetProperty(ref play1, value); }
        }
        string play2 = string.Empty;
        public string Play2
        {
            get { return play2; }
            set { SetProperty(ref play2, value); }
        }
        string play3 = string.Empty;
        public string Play3
        {
            get { return play3; }
            set { SetProperty(ref play3, value); }
        }
        string play4 = string.Empty;
        public string Play4
        {
            get { return play4; }
            set { SetProperty(ref play4, value); }
        }
        string play5 = string.Empty;
        public string Play5
        {
            get { return play5; }
            set { SetProperty(ref play5, value); }
        }
        string play6 = string.Empty;
        public string Play6
        {
            get { return play6; }
            set { SetProperty(ref play6, value); }
        }
        string play7 = string.Empty;
        public string Play7
        {
            get { return play7; }
            set { SetProperty(ref play7, value); }
        }
        string play8 = string.Empty;
        public string Play8
        {
            get { return play8; }
            set { SetProperty(ref play8, value); }
        }

        Command resetCommand;
        public Command ResetCommand
        {
            get
            {
                return resetCommand ??
                  (resetCommand = new Command(async () =>
                  {
                      //check if game is over
                      if(!GameOver)
                      {
                          var result = await UserDialogs.Instance.ConfirmAsync("The game isn't over yet, are you sure you want to reset the game?", "Reset?");
                          if (!result)
                              return;
                      }

                      Analytics.TrackEvent("GameReset", new Dictionary<string, string>
                      {
                          ["WasFinished"] = GameOver ? "Yes" : "No"
                      });

                      CurrentGame = new string[3, 3];
                      Play0 = string.Empty;
                      Play1 = string.Empty;
                      Play2 = string.Empty;
                      Play3 = string.Empty;
                      Play4 = string.Empty;
                      Play5 = string.Empty;
                      Play6 = string.Empty;
                      Play7 = string.Empty;
                      Play8 = string.Empty;
                      Player1Up = true;
                      GameOver = false;
                      Moves = 0;
                      CurrentStatus = $"{Settings.Player1} is up.";
                  }));
            }
        }

        Command<string> playCommand;
        public Command<string> PlayCommand
        {
            get
            {
                return playCommand ??
                    (playCommand = new Command<string>(async (p) => await Play(p)));
            }
        }


        async Task Play(string number)
        {
            if (GameOver)
                return;

            int x = 0;
            int y = 0;
            switch(number)
            {
                case "0":
                    break;
                case "1":
                    x = 1;
                    y = 0;
                    break;
                case "2":
                    x = 2;
                    y = 0;
                    break;
                case "3":
                    x = 0;
                    y = 1;
                    break;
                case "4":
                    x = 1;
                    y = 1;
                    break;
                case "5":
                    x = 2;
                    y = 1;
                    break;
                case "6":
                    x = 0;
                    y = 2;
                    break;
                case "7":
                    x = 1;
                    y = 2;
                    break;
                case "8":
                    x = 2;
                    y = 2;
                    break;
                default:
                    return;
            }

            if (CurrentGame[x, y] != null)
                return;

            if (Player1Up)
            {
                CurrentGame[x, y] = "X";
                CurrentStatus = $"{Settings.Player2} is up.";
            }
            else
            {
                CurrentGame[x, y] = "O";
                CurrentStatus = $"{Settings.Player1} is up.";
            }

            Player1Up = !Player1Up;

            switch (number)
            {
                case "0":
                    Play0 = CurrentGame[x, y];
                    break;
                case "1":
                    Play1 = CurrentGame[x, y];
                    break;
                case "2":
                    Play2 = CurrentGame[x, y];
                    break;
                case "3":
                    Play3 = CurrentGame[x, y];
                    break;
                case "4":
                    Play4 = CurrentGame[x, y];
                    break;
                case "5":
                    Play5 = CurrentGame[x, y];
                    break;
                case "6":
                    Play6 = CurrentGame[x, y];
                    break;
                case "7":
                    Play7 = CurrentGame[x, y];
                    break;
                case "8":
                    Play8 = CurrentGame[x, y];
                    break;
                default:
                    return;
            }

            Analytics.TrackEvent($"Move{Moves}", new Dictionary<string, string>
            {
                ["Play"] = number
            });

            Moves++;
            await CheckResults();
        }
        //check for win or draw.
        async Task CheckResults()
        {
            //Conditions for winning the game
            string[,] winningCombos =
            {
                {CurrentGame[0,0], CurrentGame[0,1], CurrentGame[0,2]},
                {CurrentGame[1,0], CurrentGame[1,1], CurrentGame[1,2]},
                {CurrentGame[2,0], CurrentGame[2,1], CurrentGame[2,2]},
                {CurrentGame[0,0], CurrentGame[1,0], CurrentGame[2,0]},
                {CurrentGame[0,1], CurrentGame[1,1], CurrentGame[2,1]},
                {CurrentGame[0,2], CurrentGame[1,2], CurrentGame[2,2]},
                {CurrentGame[0,0], CurrentGame[1,1], CurrentGame[2,2]},
                {CurrentGame[0,2], CurrentGame[1,1], CurrentGame[2,0]}
            };

            for (int i = 0; i < 8; i++)
            {

                if ((winningCombos[i, 0] == "X") & (winningCombos[i, 1] == "X") & (winningCombos[i, 2] == "X"))
                {
                    GameOver = true;
                    CurrentStatus = $"{Settings.Player1} Won!";
                    await InsertWinner(1);
                    return;
                }

                if ((winningCombos[i, 0] == "O") & (winningCombos[i, 1] == "O") & (winningCombos[i, 2] == "O"))
                {
                    GameOver = true;
                    CurrentStatus = $"{Settings.Player2} Won!";
                    await InsertWinner(2);
                    return;
                }

            }


            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (CurrentGame[x, y] == null)
                        return;
                }
            }

            //Draw!
            GameOver = true;
            CurrentStatus = "Game is a draw!";
            await InsertWinner(0);
        }

        async Task InsertWinner(int winner)
        {
            var winnerName = string.Empty;
            bool isDraw = false;
            var date = DateTime.UtcNow;
            switch(winner)
            {
                case 0:
                    isDraw = true;
                    
                    await UserDialogs.Instance.AlertAsync("Game is a draw! Game has been recorded. Hit reset to start a new game.", "Draw!");
                    break;
                case 1:
                    winnerName = Settings.Player1;
                    await UserDialogs.Instance.AlertAsync($"{Settings.Player1} won this game! Game has been recorded. Hit reset to start a new game.", $"{Settings.Player1} Wins!");
                    break;
                case 2:
                    winnerName = Settings.Player2;
                    await UserDialogs.Instance.AlertAsync($"{Settings.Player2} won this game! Game has been recorded. Hit reset to start a new game.", $"{Settings.Player2} Wins!");
                    break;
            }

            Analytics.TrackEvent("GameFinished", new Dictionary<string, string>
            {
                ["Winner"] = winner == 1 ? "X" : winner == 2 ? "O" : "Draw",
                ["Moves"] = Moves.ToString()
            });

            var game = new Game
            {
                Winner = winnerName,
                Player1 = Settings.Player1,
                Player2 = Settings.Player2,
                DateUtc = date,
                IsDraw = isDraw,
                Moves = Moves
            };

            var progress = UserDialogs.Instance.Loading("Saving game...", maskType: MaskType.Gradient);
            try
            {
                IsBusy = true;
                await DependencyService.Get<AzureService>().Add(game);
            }
            catch
            {
            }
            finally
            {
                progress.Hide();
                IsBusy = false;
            }

        }
    }
}
