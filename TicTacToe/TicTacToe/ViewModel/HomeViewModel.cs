using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using Acr.UserDialogs;
using TicTacToe.View;

namespace TicTacToe
{
    public class HomeViewModel : ViewModelBase
    {

        public HomeViewModel(Page page) : base(page)
        {

        }
        
        public string Player1
        {
            get { return Settings.Player1;  }
            set
            {
                Settings.Player1 = value;
                StartGameCommand.ChangeCanExecute();
            }
        }

        public string Player2
        {
            get { return Settings.Player2; }
            set
            {
                Settings.Player2 = value;
                StartGameCommand.ChangeCanExecute();
            }
        }

        Command startGameCommand;
        public Command StartGameCommand
        {
            get
            {
                return startGameCommand ??
                    (startGameCommand = new Command(async () => await StartGame(), 
                    () =>
                    {
                        if (string.IsNullOrWhiteSpace(Settings.Player1) || string.IsNullOrWhiteSpace(Settings.Player2))
                            return false;

                        return true;
                    }));
            }
        }

        Command historyCommand;
        public Command HistoryCommand
        {
            get
            {
                return historyCommand ??
                    (historyCommand = new Command(async () =>
                    {
                        await Page.Navigation.PushAsync(new HistoryPage());
                    }));
                   
            }
        }

        async Task StartGame()
        {
            if(string.IsNullOrWhiteSpace(Settings.Player1) || string.IsNullOrWhiteSpace(Settings.Player2))
            {
                await UserDialogs.Instance.AlertAsync("Please fill in both player's names to start.", "Uh oh! :(");
                return;
            }

            await Page.Navigation.PushAsync(new GamePage());
        }

    }
}
