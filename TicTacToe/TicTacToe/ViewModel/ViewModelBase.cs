using MvvmHelpers;
using Xamarin.Forms;

namespace TicTacToe
{
    public class ViewModelBase : BaseViewModel
    {
        protected Page Page { get; }
        public ViewModelBase(Page page)
        {
            Page = page;
        }

        public Settings Settings
        {
            get { return Settings.Current; }
        }



    }
}
