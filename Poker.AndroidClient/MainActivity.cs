using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Widget;

using Poker.WebSocketsClient;

namespace Poker.AndroidClient
{
    [Activity(Label = "Poker.AndroidClient", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView textView;
        ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            textView = FindViewById<TextView>(Resource.Id.main_labelConnecting);
            progressBar = FindViewById<ProgressBar>(Resource.Id.main_progressBar);
            Task.Run(() => ConnectToServer());
        }

        private async Task ConnectToServer()
        {
            var labelText = GetString(Resource.String.main_connected);
            try
            {
                await WebSocketClient.Start();
                await WebSocketClient.ConnectToServerAsync("AndroidClient");
            }
            catch (System.Exception)
            {
                labelText = GetString(Resource.String.main_cannot_connect);
            }
            RunOnUiThread(() =>
            {
                textView.Text = labelText;
                progressBar.Visibility = Android.Views.ViewStates.Invisible;
            });

            await Task.Factory.StartNew(() => {
                StartActivity(typeof(DashBoardActivity));
            });
            await Task.Delay(1500);
        }
    }
}

