using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using Poker.WebSocketsClient;
using Poker.WebSocketsClient.Enums;

namespace Poker.AndroidClient
{
    [Activity(Label = "DashBoardActivity")]
    public class DashBoardActivity : Activity
    {
        private ListView listView;
        private Button sendButton;
        private EditText messageInput;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DashBoard);

            listView = FindViewById<ListView>(Resource.Id.dashBoard_lvMessages);
            sendButton = FindViewById<Button>(Resource.Id.dashBoard_btnSend);
            messageInput = FindViewById<EditText>(Resource.Id.dashBoard_etMessageText);

            ConfigureLogic();
            ConfigureMessageInput();
            ConfigureButton();
        }

        private void ConfigureLogic()
        {
            var messages = new List<string>();
            var listViewAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleExpandableListItem1, messages);
            listView.Adapter = listViewAdapter;

            Action<string> sendMessageCallback = (message) =>
            {
                RunOnUiThread(() => {
                    listViewAdapter.Add(message);
                    listViewAdapter.NotifyDataSetChanged();
                });
            };

            WebSocketClient.AddCallBack(ServerMethod.SendMessage, sendMessageCallback);
        }

        private void ConfigureMessageInput()
        {
            messageInput.AfterTextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(messageInput.Text))
                {
                    sendButton.Enabled = false;
                }
                else
                {
                    sendButton.Enabled = true;
                }
            };
        }

        private void ConfigureButton()
        {
            sendButton.Click += async (s, e) =>
            {
                await WebSocketClient.InvokeAsync(ServerMethod.SendMessage, messageInput.Text);
                messageInput.Text = string.Empty;
            };
        }
    }
}