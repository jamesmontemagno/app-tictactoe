using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe.View
{
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel vm;
        public HistoryPage()
        {
			InitializeComponent ();
            BindingContext = vm = new HistoryViewModel(this);
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.RefreshDataCommand.Execute(null);
        }
    }
}
