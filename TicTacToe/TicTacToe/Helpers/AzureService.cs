using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;
using Xamarin.Forms;
using TicTacToe;
using Plugin.Connectivity;



[assembly: Dependency(typeof(AzureService))]
namespace TicTacToe
{
    public class AzureService
    {

        public MobileServiceClient Client { get; set; } = null;
        IMobileServiceSyncTable<Game> table;

        public async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            CurrentPlatform.Init();


            var appUrl = "https://mobile-13f2a020-9809-42ce-8db2-783ee8c05e68.azurewebsites.net";


            Client = new MobileServiceClient(appUrl);

            //InitialzeDatabase for path
            var path = "syncstore.db";
  
            //setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);

            //Define table
            store.DefineTable<Game>();


            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store);

            //Get our sync table that will call out to azure
            table = Client.GetSyncTable<Game>();


        }

        public async Task Sync()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

#if !DEBUG

                await table.PullAsync("all", table.CreateQuery());

                await Client.SyncContext.PushAsync();
#endif
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }

        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            //Initialize & Sync
            await Initialize();
            await Sync();

            return await table.OrderBy(c => c.DateUtc).ToEnumerableAsync(); ;

        }

        public async Task<Game> Add(Game game)
        {
            await Initialize();

            await table.InsertAsync(game);

            await Sync();

            return game;
        }
        
    }
}

