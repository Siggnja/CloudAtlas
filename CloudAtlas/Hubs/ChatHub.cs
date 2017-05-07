using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace CloudAtlas
{
    public class ChatHub : Hub
    {

        public void ChatMessage(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.ChatMessage(name, message);
        }

        public void UpdateEditor(object changeData)
        {
            Clients.Others.UpdateEditor(changeData);
        }
    }
}