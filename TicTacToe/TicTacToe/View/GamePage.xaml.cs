using Acr.UserDialogs;
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
		}

        protected override bool OnBackButtonPressed()
        {
            UserDialogs.Instance.ConfirmAsync("Are you sure you want to leave your current game?", "Leave game").ContinueWith(async (exit) =>
            {
                if (exit.Result)
                    await Navigation.PopAsync();
            });

            return true;
        }
    }
}
