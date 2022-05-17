using Chat.Models;
using Microsoft.AspNetCore.SignalR;
 
namespace SignalRApp
{
    public class ChatHub : Hub
    {
        public static IDictionary<string, int> GroupsClients = new Dictionary<string, int>();
        
        public async Task Send(string group, Message message)
        {
            await this.Clients.Group(group).SendAsync("receive", message);
        }

        public async Task Connect(string group, string username) {
            bool isChanged = false;
            lock(GroupsClients) {
                if(GroupsClients.ContainsKey(group)) {
                    GroupsClients[group]++;
                }
                else {
                    GroupsClients.Add(group, 1);
                    isChanged = true;
                }
            }
            if(isChanged) await SendGroups();
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("notify", $"Пользователь {username} вошел в группу");
        }

        public async Task Disconnect(string group, string username) {
            bool isChanged = false;
                lock(GroupsClients) {
                GroupsClients[group]--;
                if(GroupsClients[group] == 0) {
                    GroupsClients.Remove(group);
                    isChanged = true;
                }
            }
            if(isChanged) await SendGroups();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("notify", $"Пользователь {username} вышел из группы");
        }

        public async Task SendGroups() {
            await Clients.All.SendAsync("getGroups", GroupsClients.Keys);
        }
    }
}