﻿using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace CloudAtlas
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Others.addNewMessageToPage(message);
        }
        public void SendLine(int line, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Others.addNewMessageToPageLine(line, message);
        }
        public void Send2(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage2(name, message);
        }

        public void OnChange(object changeData)
        {
            Clients.Others.OnChange(changeData);
        }
    }
}