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
	}
}
