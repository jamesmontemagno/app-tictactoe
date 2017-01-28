using Acr.UserDialogs;
using Microsoft.Azure.Mobile.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TicTacToe.View
{
	public partial class GamePage : ContentPage
	{
		public GamePage ()
		{
			InitializeComponent ();
            BindingContext = new GameViewModel(this);
            Title = $"{Settings.Current.Player1} vs. {Settings.Current.Player2}";
            Analytics.TrackEvent("Navigation", new Dictionary<string, string>
            {
                ["Page"] = "Game"
            });
        }

        protected override bool OnBackButtonPressed()
        {
            UserDialogs.Instance.ConfirmAsync("Are you sure you want to leave your current game?", "Leave game").ContinueWith((exit) =>
            {
                if (exit.Result)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                    });
                }
            });

            return true;
        }
    }
}
