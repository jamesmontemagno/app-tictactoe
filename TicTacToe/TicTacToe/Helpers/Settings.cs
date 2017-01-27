
// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace TicTacToe
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public class Settings : ViewModelBase
    {
        public Settings(Page page = null) : base(null)
        {

        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        static Settings settings;
        public static Settings Current
        {
            get { return settings ?? (settings = new Settings()); }
        }

        #region Setting Constants

        private const string Player1Key = "player1_key";
        private static readonly string Player1Default = string.Empty;

        private const string Player2Key = "player2_key";
        private static readonly string Player2Default = string.Empty;

        #endregion

        public string Player1
        {
            get { return AppSettings.GetValueOrDefault<string>(Player1Key, Player1Default); }
            set
            {
                if (AppSettings.AddOrUpdateValue<string>(Player1Key, value))
                    OnPropertyChanged();
            }
        }

        public string Player2
        {
            get { return AppSettings.GetValueOrDefault<string>(Player2Key, Player2Default); }
            set
            {
                if (AppSettings.AddOrUpdateValue<string>(Player2Key, value))
                    OnPropertyChanged();
            }
        }
        
    }
}