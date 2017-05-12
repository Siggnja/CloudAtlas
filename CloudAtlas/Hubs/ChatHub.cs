using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace CloudAtlas
{
    public class ChatHub : Hub
    {

        public void JoinFile(int fileID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(fileID));
        }

        public void LeaveFile(int fileID)
        {
            Groups.Remove(Context.ConnectionId, Convert.ToString(fileID));
        }

        public void SwitchFile(int oldID, int newID)
        {
            Groups.Remove(Context.ConnectionId, Convert.ToString(oldID));
            Groups.Add(Context.ConnectionId, Convert.ToString(newID));
        }

        public void JoinChat(int projectID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(projectID));
        }

        public void ChatMessage(string name, string message, int projectID)
        {
            Clients.Group(Convert.ToString(projectID)).ChatMessage(name, message);
        }

        public void UpdateEditor(object changeData, int fileID, string userID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).UpdateEditor(changeData, userID);
        }
    }
}