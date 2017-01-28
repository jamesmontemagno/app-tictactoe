using Acr.UserDialogs;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TicTacToe
{
    public class HistoryViewModel : ViewModelBase
    {
        public HistoryViewModel(Page page) :base(page)
        {
            Items = new ObservableRangeCollection<Game>();
            RefreshDataCommand = new Command(
              async () => await RefreshData());
        }

        public ObservableRangeCollection<Game> Items { get; }
     

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            var progress = UserDialogs.Instance.Loading("Loading history...", maskType: MaskType.Gradient);
            try
            {
                var items = await DependencyService.Get<AzureService>().GetGames();
                Items.ReplaceRange(items);
            }
            catch(Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Unable to query history, try again.");
                return;
            }
            finally
            {
                progress.Hide();
                IsBusy = false;
            }

            if(Items.Count == 0)
                await UserDialogs.Instance.AlertAsync("No games have been played yet, go play some!", "No History");
        }


    }
}
